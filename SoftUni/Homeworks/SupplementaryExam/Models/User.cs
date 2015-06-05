namespace IssueTracker.Models
{
    using Interfaces;
    using System.Collections.Generic;

    public class User : IUser
    {
        private string username;
        private string password;
        private ICollection<IIssue> issues;
        private ICollection<IComment> comments;
        private bool isLogged;

        public User(string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Issues = new HashSet<IIssue>();
            this.Comments = new HashSet<IComment>();
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public bool IsLogged
        {
            get
            {
                return this.isLogged;
            }
            set
            {
                this.isLogged = value;
            }
        }

        public ICollection<IIssue> Issues
        {
            get { return this.issues; }
            set { this.issues = value; }
        }

        public ICollection<IComment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }
    }
}
