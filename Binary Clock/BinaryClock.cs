using System;

namespace Binary_Clock
{
    public static class BinaryClock
    {
        public static string Hour => Convert.ToString(DateTime.Now.Hour, 2);
        public static string Minute => Convert.ToString(DateTime.Now.Minute, 2);
        public static string Second => Convert.ToString(DateTime.Now.Second, 2);
        public static Clock Now => new Clock();

        public struct Clock
        {
            public string HourFirstDigit = Convert.ToString(DateTime.Now.Hour / 10, 2);
            public string HourSecondDigit = Convert.ToString(DateTime.Now.Hour % 10, 2);

            public string MinuteFirstDigit = Convert.ToString(DateTime.Now.Minute / 10, 2);
            public string MinuteSecondDigit = Convert.ToString(DateTime.Now.Minute % 10, 2);

            public string SecondFirstDigit = Convert.ToString(DateTime.Now.Second / 10, 2);
            public string SecondSecondDigit = Convert.ToString(DateTime.Now.Second % 10, 2);
        }
    }
}
