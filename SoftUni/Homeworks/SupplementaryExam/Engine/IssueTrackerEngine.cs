using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Engine.Factories;
using IssueTracker.Interfaces;
using IssueTracker.Models;
using Microsoft.SqlServer.Server;

namespace IssueTracker.Engine
{
    using Interfaces.Engine;
    using System.Security.Cryptography;
    using System.Text;

    public class IssueTrackerEngine : IIssueTrackerEngine, IIssueTracker
    {
        private static IIssueTrackerEngine instance;

        private readonly IIssueFactory issueFactory;
        private readonly IUserFactory userFactory;
        private readonly ICommentFactory commentFactory;

        private readonly IDictionary<string, IUser> users;
        private readonly IDictionary<string, IIssue> issues;
        private readonly IDictionary<string, IComment> comments;

        private readonly IUserInterface userInterface;

        private IssueTrackerEngine()
        {
            this.issueFactory = new IssueFactory();
            this.userFactory = new UserFactory();
            this.commentFactory = new CommentFactory();
            this.users = new Dictionary<string, IUser>();
            this.issues = new Dictionary<string, IIssue>();
            this.comments = new Dictionary<string, IComment>();
            this.userInterface = new ConsoleInterface();
        }

        public static IIssueTrackerEngine Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IssueTrackerEngine();
                }

                return instance;
            }
        }

        public void Start()
        {
            var commands = this.ReadCommands();
            var processCommands = this.ProcessCommands(commands);
            this.userInterface.Output(processCommands);
        }

        private ICollection<ICommand> ReadCommands()
        {
            var commands = new List<ICommand>();
            foreach (var line in this.userInterface.Input())
            {
                commands.Add(Command.Parse(line));
            }

            return commands;
        }

        private IEnumerable<string> ProcessCommands(ICollection<ICommand> commands)
        {
            var commandResults = new List<string>();

            foreach (var command in commands)
            {
                string commandResult;
                switch (command.Name)
                {
                    case IssueTrackerConstants.RegisterUser:
                        commandResult = this.RegisterUser(command.Parameters["username"],
                            command.Parameters["password"],
                            command.Parameters["confirmPassword"]);
                        break;
                    case IssueTrackerConstants.LoginUser:
                        commandResult = this.LoginUser(command.Parameters["username"],
                            command.Parameters["password"]);
                        break;
                    case IssueTrackerConstants.LogoutUser:
                        commandResult = this.LogoutUser();
                        break;
                    case IssueTrackerConstants.CreateIssue:
                        commandResult = this.CreateIssue(command.Parameters["title"], 
                            command.Parameters["description"],
                            this.PriorityParse(command.Parameters["priority"]),
                            command.Parameters["tags"].Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries));
                        break;
                    case IssueTrackerConstants.RemoveIssue:
                        commandResult = this.RemoveIssue(int.Parse(command.Parameters["id"]));
                        break;
                    case IssueTrackerConstants.AddComment:
                        commandResult =
                            this.AddComment(int.Parse(command.Parameters["id"]), command.Parameters["text"]);
                        break;
                    case IssueTrackerConstants.MyIssues:
                        commandResult = this.GetMyIssues();
                        break;
                    case IssueTrackerConstants.MyComments:
                        commandResult = this.GetMyComments();
                        break;
                    case IssueTrackerConstants.Search:
                        commandResult = this.SearchForIssues(command.Parameters["tags"].Split(new []{'|'}, StringSplitOptions.RemoveEmptyEntries));
                        break;
                    default:
                        throw new InvalidOperationException(string.Format("Invalid action: {0}", command.Name));
                }

                commandResults.Add(commandResult);
            }

            return commandResults;
        }

        public static
            string Sha1HashUserPassword(string password)
        {
            byte[] passwordToBytes = Encoding.UTF8.GetBytes(password);
 
            var sha1 = SHA1.Create();
            byte[] hashBytes = sha1.ComputeHash(passwordToBytes);
 
            return HexStringFromBytes(hashBytes);
        }

        public static string HexStringFromBytes(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }

            return sb.ToString();
        }

        public string RegisterUser(string username, string password, string confirmPassword)
        {
            if (this.IsThareLoggedUser())
            {
                return IssueTrackerConstants.AllreadyLoggedUser;
            }

            bool arePasswordsDifferent = (password != confirmPassword);
            if (arePasswordsDifferent)
            {
                return IssueTrackerConstants.PasswordsDoNotMatch;
            }

            if (this.IsUserExists(username))
            {
                return string.Format(IssueTrackerConstants.UserAllreadyExists, username);
            }

            string hashedPassword = Sha1HashUserPassword(password);
            this.users[username] = new User(username, hashedPassword);

            return string.Format(IssueTrackerConstants.RegistredSuccessfuly, username);
        }

        public string LoginUser(string username, string password)
        {
            if (this.IsThareLoggedUser())
            {
                return IssueTrackerConstants.AllreadyLoggedUser;
            }

            if (!this.IsUserExists(username))
            {
                return string.Format(IssueTrackerConstants.UsernameDoesNotExists, username);
            }

            bool userExistsAndPasswordMatch =
                this.users.Any(x => x.Key == username && x.Value.Password == Sha1HashUserPassword(password));

            if (userExistsAndPasswordMatch)
            {
                this.users[username].IsLogged = true;
                return string.Format(IssueTrackerConstants.LoginSuccessfuly, username);
            }

            return string.Format(IssueTrackerConstants.InvalidPassword, username);
        }

        public string LogoutUser()
        {
            var user = this.users.Where(x => x.Value.IsLogged);

            if (user.Any())
            {
                var currentUser = user.FirstOrDefault();
                currentUser.Value.IsLogged = false;

                return string.Format(IssueTrackerConstants.LogoutSuccessfuly, currentUser.Value.Username);
            }

            return IssueTrackerConstants.ThereIsNoLoggedUser;
        }

        public string CreateIssue(string title, string description, IssuePriority priority, string[] tags)
        {
            string titleParam = "title";
            string descriptionParam = "description";
            int titleMinLength = 3;
            int descriptionMinLength = 5;

            if (title.Length < 3)
            {
                throw new ArgumentException(string.Format(IssueTrackerConstants.IssueParamsLength, titleParam, titleMinLength));
            }

            if (description.Length < 5)
            {
                throw new ArgumentException(string.Format(IssueTrackerConstants.IssueParamsLength, descriptionParam, descriptionMinLength));
            }

            if (!this.IsThareLoggedUser())
            {
                return IssueTrackerConstants.ThereIsNoLoggedUser;
            }

            int currentIssueId = this.CreateIssueId();
            string currentIssueIdAsStringKey = string.Format("{0}", currentIssueId);

            this.issues[currentIssueIdAsStringKey] = new Issue(currentIssueId, title, description, this.GetLoggedUser(), priority);
            var currentIssue = this.issues[currentIssueIdAsStringKey];
            currentIssue.Tags = tags.Distinct().ToArray();

            this.GetLoggedUser().Issues.Add(currentIssue);

            return string.Format(IssueTrackerConstants.IssueCreatedSuccessfuly, currentIssueId);
        }

        public string RemoveIssue(int issueId)
        {
            if (!this.IsThareLoggedUser())
            {
                return IssueTrackerConstants.ThereIsNoLoggedUser;
            }

            if (!this.IsThereIssueWithSuchId(issueId))
            {
                return string.Format(IssueTrackerConstants.ThereIsNoIssueWithId, issueId);
            }

            if (!this.IsIssueBelongsToLoggedUser(issueId))
            {
                return string.Format(IssueTrackerConstants.IssueDoesntBelongToUser, issueId, this.GetLoggedUser().Username);
            }

            this.issues.Remove(string.Format("{0}", issueId));
            return string.Format(IssueTrackerConstants.IssueRemovedSuccessfuly, issueId);
        }

        public string AddComment(int issueId, string text)
        {
            if (text.Length < 2)
            {
                throw new ArgumentException(IssueTrackerConstants.CommentLengthError);
            }

            if (!this.IsThareLoggedUser())
            {
                return IssueTrackerConstants.ThereIsNoLoggedUser;
            }

            if (!this.IsThereIssueWithSuchId(issueId))
            {
                return string.Format(IssueTrackerConstants.ThereIsNoIssueWithId, issueId);
            }

            var issue = this.issues[string.Format("{0}", issueId)];
            var comment = new Comment(text, this.GetLoggedUser(), issue);
            issue.Comments.Add(comment);
            this.GetLoggedUser().Comments.Add(comment);

            return string.Format(IssueTrackerConstants.CommentAddedSuccessfuly, issueId);
        }

        public string GetMyIssues()
        {
            if (!this.IsThareLoggedUser())
            {
                return IssueTrackerConstants.ThereIsNoLoggedUser;
            }

            if (!this.GetLoggedUser().Issues.Any())
            {
                return IssueTrackerConstants.NoIssues;
            }

            var sortedIssues = this.GetLoggedUser().Issues.OrderByDescending(x => x.Priority).ThenBy(x => x.Title);
            return string.Join("\n", sortedIssues);
        }

        public string GetMyComments()
        {
            if (!this.IsThareLoggedUser())
            {
                return IssueTrackerConstants.ThereIsNoLoggedUser;
            }

            if (!this.GetLoggedUser().Comments.Any())
            {
                return IssueTrackerConstants.NoComments;
            }

            var userComments = this.GetLoggedUser().Comments;

            StringBuilder output = new StringBuilder();

            output.AppendLine("Comments:");
            output.AppendLine(string.Join("\n", userComments));

            return output.ToString().Trim();
        }

        public string SearchForIssues(string[] tags)
        {
            if (!tags.Any())
            {
                return IssueTrackerConstants.NoTagsProvided;
            }

            string[] uniqueTags = tags.Distinct().ToArray();

            if (!this.issues.Any(x => x.Value.Tags.Intersect(uniqueTags).Any()))
            {
                return IssueTrackerConstants.NoIssuesByTags;
            }

            IDictionary<string, IIssue> searchedIssues = new Dictionary<string, IIssue>();

            foreach (var issue in this.issues)
            {
                foreach (var tag in uniqueTags)
                {
                    if (issue.Value.Tags.Contains(tag))
                    {
                        searchedIssues.Add(issue);
                    }
                }
            }

            var sortedIssues = searchedIssues.OrderByDescending(x => x.Value.Priority).ThenBy(x => x.Value.Title);

            StringBuilder output = new StringBuilder();

            output.AppendLine(string.Join("\n", sortedIssues));

            return output.ToString().Trim();
        }

        private bool IsThareLoggedUser()
        {
            bool isThareLoggedUser = this.users.Any(x => x.Value.IsLogged);
            if (isThareLoggedUser)
            {
                return true;
            }

            return false;
        }

        private bool IsUserExists(string username)
        {
            bool isUserExists = this.users.Any(x => x.Key == username);
            if (isUserExists)
            {
                return true;
            }

            return false;
        }

        private IUser GetLoggedUser()
        {
            if (IsThareLoggedUser())
            {
                var user = this.users.FirstOrDefault(x => x.Value.IsLogged);
                string username = user.Value.Username;

                return this.users[username];
            }

            return null;
        }

        private IssuePriority PriorityParse(string priorityParam)
        {
            IssuePriority issuePriorirty = (IssuePriority)Enum.Parse(typeof(IssuePriority), priorityParam, true);
            return issuePriorirty;
        }

        private bool IsThereIssueWithSuchId(int issueId)
        {
            if (this.issues.Any(x => int.Parse(x.Key) == issueId))
            {
                return true;
            }

            return false;
        }

        private bool IsIssueBelongsToLoggedUser(int issueId)
        {
            var issue = this.issues[string.Format("{0}", issueId)];

            if (issue.Author.Username == this.GetLoggedUser().Username)
            {
                return true;
            }

            return false;
        }

        private int CreateIssueId()
        {
            int counter = 1;

            while (this.issues.Any(x => int.Parse(x.Key) == counter))
            {
                counter++;
            }

            return counter;
        }
    }
}
