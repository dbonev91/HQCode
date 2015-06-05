namespace IssueTracker.Engine.Factories
{
    using Interfaces.Engine;
    using Interfaces;
    using Models;

    public class IssueFactory : IIssueFactory
    {
        public IIssue CreateIssue(int id, string title, string description, IUser author, IssuePriority priority)
        {
            var issue = new Issue(id, title, description, author, priority);
            return issue;
        }
    }
}
