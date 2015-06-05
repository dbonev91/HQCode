namespace IssueTracker.Interfaces.Engine
{
    public interface IUserFactory
    {
        IUser CreateUser(string username, string password);
    }
}
