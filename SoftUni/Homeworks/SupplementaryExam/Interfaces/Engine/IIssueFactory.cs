namespace IssueTracker.Interfaces.Engine
{
    using Models;

    public interface IIssueFactory
    {
        IIssue CreateIssue(int id, string title, string description, IUser author, IssuePriority priority);
    }
}
