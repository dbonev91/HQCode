using System;
using System.Linq;

namespace IssueTracker.Models
{
    using Interfaces;
    using System.Text;
    using System.Collections.Generic;

    public class Issue : IIssue
    {
        private int id;
        private string title;
        private string description;
        private IUser author;
        private IssuePriority issuePriority;
        private ICollection<string> tags;
        private ICollection<IComment> comments;

        public Issue(int id, string title, string description, IUser author, IssuePriority priority)
        {
            this.Id = id;
            this.Title = title;
            this.Description = description;
            this.Author = author;
            this.Priority = priority;
            this.Tags = new HashSet<string>();
            this.Comments = new HashSet<IComment>();
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }

        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public IUser Author
        {
            get { return this.author; }
            set { this.author = value; }
        }

        public IssuePriority Priority
        {
            get { return this.issuePriority; }
            set { this.issuePriority = value; }
        }

        public ICollection<string> Tags
        {
            get { return this.tags; }
            set { this.tags = value; }
        }

        public ICollection<IComment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            output.AppendLine(this.Title);
            output.AppendLine(string.Format("Priority: {0}", this.PriorityToStars(this.Priority)));
            output.AppendLine(this.Description);
            output.AppendLine(string.Format("Tags: {0}", string.Join(",", this.Tags)));
            output.AppendLine(string.Format("Comments:\n{0}", string.Join("\n", this.Comments)));

            return output.ToString().Trim();
        }

        private string PriorityToStars(IssuePriority priority)
        {
            switch (priority)
            {
                case IssuePriority.Low:
                    return "*";
                case IssuePriority.Medium:
                    return "**";
                case IssuePriority.High:
                    return "***";
                case IssuePriority.Showstopper:
                    return "****";
                default:
                    throw new ArgumentException("Invalid priority");
            }
        }
    }
}
