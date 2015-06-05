namespace IssueTracker.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface IIssue
    {
        int Id { get; }

        string Title { get; }

        string Description { get; }

        IUser Author { get; }

        IssuePriority Priority { get; }

        ICollection<string> Tags { get; set; }

        ICollection<IComment> Comments { get; set; }
    }
}
