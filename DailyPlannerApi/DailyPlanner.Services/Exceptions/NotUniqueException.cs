using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Services.Exceptions
{
    public class NotUniqueException : PlannerException
    {
        public NotUniqueException() : base("Data is not unique") { }
    }
}
