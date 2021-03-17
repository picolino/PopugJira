using System;

namespace PopugJira.Common
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
        DateTime Today { get; }
        DateTime UtcNow { get; }
    }
}