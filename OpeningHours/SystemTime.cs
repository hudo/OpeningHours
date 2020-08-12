using System;

namespace OpeningHours
{
    internal struct SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
