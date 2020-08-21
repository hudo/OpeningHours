using System;

namespace OpeningHours
{
    public class Settings
    {
        private int? _lunchBreakDurationMin = 30;

        public Time FromHour { get; set; }
        public Time ToHour { get; set; }

        public string Message { get; set; } = "The web is closed";

        public Time LunchBreakAtHour { get; set; }

        public int? LunchBreakDurationMin 
        { 
            get => _lunchBreakDurationMin;
            set => _lunchBreakDurationMin = value.GetValueOrDefault(-1) < 60 * 25
                ? value
                : throw new ArgumentOutOfRangeException("Too much break for lunch");
    }

        public int StatusCode { get; set; } = 412; // precondition failed? 

        /// <summary>
        /// Serve requests outside of business hours
        /// </summary>
        public string Bribe { get; set; }

        public DayOfWeek[] ClosedWeekdays { get; set; } = new DayOfWeek[0];

        public DateTime[] Holidays { get; set; } = new DateTime[0];
    }
}
