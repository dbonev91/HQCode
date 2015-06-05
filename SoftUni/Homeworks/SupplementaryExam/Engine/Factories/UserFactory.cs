namespace IssueTracker.Engine.Factories
{
    using Interfaces.Engine;
    using Interfaces;
    using Models;

    public class UserFactory : IUserFactory
    {
        public IUser CreateUser(string username, string password)
        {
            var user = new User(username, password);
            return user;
        }
    }
}
