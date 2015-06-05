using System.Collections.Generic;

namespace IssueTracker.Interfaces
{
    public interface IUser
    {
        string Username { get; }

        string Password { get; }

        bool IsLogged { get; set; }

        ICollection<IIssue> Issues { get; set; }

        ICollection<IComment> Comments { get; set; }
    }
}
