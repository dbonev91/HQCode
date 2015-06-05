using System;

namespace IssueTracker.Models
{
    using System.Text;
    using Interfaces;

    public class Comment : IComment
    {
        private string text;
        private IUser author;
        private IIssue issue;

        public Comment(string text, IUser author, IIssue issue)
        {
            this.Text = text;
            this.Author = author;
            this.Issue = issue;
        }

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public IUser Author
        {
            get { return this.author; }
            set { this.author = value; }
        }

        public IIssue Issue
        {
            get { return this.issue; }
            set { this.issue = value; }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine(this.Text);
            output.AppendLine(string.Format("-- {0}", this.Author.Username));

            return output.ToString().Trim();
        }
    }
}
