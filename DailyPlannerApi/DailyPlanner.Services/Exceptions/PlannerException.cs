﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DailyPlanner.Services.Exceptions
{
    public class PlannerException : Exception
    {
        public PlannerException(string msg) : base(msg) { }
        public PlannerException(string msg, Exception inner) : base(msg, inner) { }
    }
}
