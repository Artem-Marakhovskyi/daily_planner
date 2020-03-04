namespace DailyPlanner.Services.Exceptions
{
    public class NotUniqueException : PlannerException
    {
        public NotUniqueException() : base("Data is not unique") { }
    }
}
