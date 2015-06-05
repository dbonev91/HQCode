namespace IssueTracker.Interfaces
{
    public interface IComment
    {
        string Text { get; }

        IUser Author { get; }

        IIssue Issue { get; }
    }
}
