namespace IssueTracker
{
    using IssueTracker.Engine;

    public class IssueTrackerStartup
    {
        public static void Main()
        {
            IssueTrackerEngine.Instance.Start();
        }
    }
}
