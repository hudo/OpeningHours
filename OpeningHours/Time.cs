using System;
using System.Text.Json.Serialization;

namespace OpeningHours
{
    [JsonConverter(typeof(TimeJsonConverter))]
    public struct Time 
    {
        public Time(int hour, int minute)
        {
            if (hour < 0 || hour > 24) throw new ArgumentOutOfRangeException("Hour out of range");
            if (minute < 0 || minute > 60) throw new ArgumentOutOfRangeException("Minute out of range");

            Hour = hour;
            Minute = minute;    
        }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public static implicit operator Time(int hour) => new Time(hour, 0);
        public static implicit operator Time(double time) => new Time(TimeSpan.FromHours(time).Hours, TimeSpan.FromHours(time).Minutes);
        public static implicit operator Time(DateTime time) => new Time(time.Hour, time.Minute);

        /// <summary>
        /// Format hour:minute
        /// </summary>
        /// <param name="time">Hour:Minute</param>
        public static implicit operator Time(string time)
        {
            if (string.IsNullOrWhiteSpace(time)) return new Time();

            var parts = time.Split(':');

            return new Time(
                Convert.ToInt32(parts[0]),
                parts.Length > 1 ? Convert.ToInt32(parts[1]) : 0);
        }

        public static bool operator >(Time first, Time second) => first.Hour > second.Hour || first.Hour == second.Hour && first.Minute > second.Minute;
        public static bool operator <(Time first, Time second) => !(first > second);
        public static bool operator ==(Time first, Time second) => first.Hour == second.Hour && first.Minute == second.Minute;
        public static bool operator !=(Time first, Time second) => !(first == second);
        public static bool operator >=(Time first, Time second) => first == second || first > second;
        public static bool operator <=(Time first, Time second) => first == second || first < second;

        public override string ToString() => $"{Hour}:{Minute}";
    }
}