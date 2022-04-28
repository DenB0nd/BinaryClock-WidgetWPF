using System;

namespace Binary_Clock;

public class BinaryClock
{
    public string Hour => Convert.ToString(DateTime.Now.Hour, 2);
    public string Minute => Convert.ToString(DateTime.Now.Minute, 2);
    public string Second => Convert.ToString(DateTime.Now.Second, 2);
    public Clock Now => new Clock();
}
