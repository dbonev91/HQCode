namespace IssueTracker.Engine.Factories
{
    using Interfaces.Engine;
    using Interfaces;
    using Models;

    public class CommentFactory : ICommentFactory
    {
        public IComment AddComment(string text, IUser author, IIssue issue)
        {
            var comment = new Comment(text, author, issue);
            return comment;
        }
    }
}
