namespace IssueTracker.Engine
{
    public static class IssueTrackerConstants
    {
        #region Commands

        public const string RegisterUser = "RegisterUser";
        public const string LoginUser = "LoginUser";
        public const string LogoutUser = "LogoutUser";
        public const string CreateIssue = "CreateIssue";
        public const string RemoveIssue = "RemoveIssue";
        public const string AddComment = "AddComment";
        public const string MyIssues = "MyIssues";
        public const string MyComments = "MyComments";
        public const string Search = "Search";

        #endregion

        #region Error messages

        public const string AllreadyLoggedUser = "There is already a logged in user";
        public const string PasswordsDoNotMatch = "The provided passwords do not match";
        public const string UserAllreadyExists = "A user with username {0} already exists";
        public const string UsernameDoesNotExists = "A user with username {0} does not exist";
        public const string InvalidPassword = "The password is invalid for user {0}";
        public const string ThereIsNoLoggedUser = "There is no currently logged in user";
        public const string IssueParamsLength = "Issue {0} should be {1} symbols at least!";
        public const string ThereIsNoIssueWithId = "There is no issue with ID {0}";
        public const string IssueDoesntBelongToUser = "The issue with ID {0} does not belong to user {1}";
        public const string CommentLengthError = "Comment length should be 2 symbols at least!";
        public const string NoTagsProvided = "There are no tags provided";
        public const string NoIssuesByTags = "There are no issues matching the tags provided";

        #endregion

        #region

        public const string RegistredSuccessfuly = "User {0} registered successfuly";
        public const string LoginSuccessfuly = "User {0} logged in successfully";
        public const string LogoutSuccessfuly = "User {0} logged out successfully";
        public const string IssueCreatedSuccessfuly = "Issue {0} created successfully";
        public const string IssueRemovedSuccessfuly = "Issue {0} removed";
        public const string CommentAddedSuccessfuly = "Comment added successfully to issue {0}";
        public const string NoIssues = "No issues";
        public const string NoComments = "No comments";

        #endregion
    }
}
