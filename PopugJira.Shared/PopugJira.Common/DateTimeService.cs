using System;
using Serviced;

namespace PopugJira.Common
{
    public class DateTimeService : IDateTimeService, IScoped<IDateTimeService>
    {
        public DateTime Now => DateTime.Now;
        public DateTime Today => DateTime.Today;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}