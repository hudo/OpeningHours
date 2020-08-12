using System;

namespace OpeningHours
{
    public class Settings
    {
        private int _fromHour;
        private int _toHour;
        private int? _lunchBreakAtHour;
        private int? _lunchBreakDurationMin = 30;

        public int FromHour
        {
            get => _fromHour;
            set => _fromHour = ValidateTime(value);
        }

        public int ToHour
        {
            get => _toHour;
            set => _toHour = ValidateTime(value);
        }

        public string Message { get; set; } 

        public int? LunchBreakAtHour
        {
            get => _lunchBreakAtHour;
            set => _lunchBreakAtHour = ValidateTime(value.GetValueOrDefault(-1));
        }

        public int? LunchBreakDurationMin 
        { 
            get => _lunchBreakDurationMin; 
            set => _lunchBreakDurationMin = ValidateTime(value.GetValueOrDefault(-1), 60*24); 
        }

        public int StatusCode { get; set; } = 412; // precondition failed? 

        private int ValidateTime(int value, int maxValue = 24) => value >= 0 && value <= maxValue ? value : throw new ArgumentOutOfRangeException($"Value can be from 0 to {maxValue}");

        internal void ValidateAllTimes()
        {
            ValidateTime(FromHour, 24);
            ValidateTime(ToHour, 24);
        }
    }
}
