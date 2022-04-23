using System;

namespace Binary_Clock;

public static class BinaryClock
{
    public static string Hour => Convert.ToString(DateTime.Now.Hour, 2);
    public static string Minute => Convert.ToString(DateTime.Now.Minute, 2);
    public static string Second => Convert.ToString(DateTime.Now.Second, 2);
    public static Clock Now => new Clock();
}
