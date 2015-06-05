namespace IssueTracker.Interfaces.Engine
{
    public interface ICommentFactory
    {
        IComment AddComment(string text, IUser author, IIssue issue);
    }
}
