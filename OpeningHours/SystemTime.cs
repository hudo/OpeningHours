using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OpeningHours.Tests")]
namespace OpeningHours
{
    
    internal struct SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
