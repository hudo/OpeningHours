using System;

namespace OpeningHours
{
    public class Time 
    {
        public Time() { }

        public Time(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;    
        }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public static implicit operator Time(int hour) => new Time(hour, 0);
        public static implicit operator Time(double time) => new Time(TimeSpan.FromHours(time).Hours, TimeSpan.FromHours(time).Minutes);
        public static implicit operator Time(DateTime time) => new Time(time.Hour, time.Minute);
    }
}