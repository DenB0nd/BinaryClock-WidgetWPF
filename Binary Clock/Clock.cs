using System;

namespace Binary_Clock;

public struct Clock
{
    private const char BINARY_ZERO = '0';

    public string HourFirstDigit = Convert.ToString(DateTime.Now.Hour / 10, 2).PadLeft(4, BINARY_ZERO);
    public string HourSecondDigit = Convert.ToString(DateTime.Now.Hour % 10, 2).PadLeft(4, BINARY_ZERO);

    public string MinuteFirstDigit = Convert.ToString(DateTime.Now.Minute / 10, 2).PadLeft(4, BINARY_ZERO);
    public string MinuteSecondDigit = Convert.ToString(DateTime.Now.Minute % 10, 2).PadLeft(4, BINARY_ZERO);

    public string SecondFirstDigit = Convert.ToString(DateTime.Now.Second / 10, 2).PadLeft(4, BINARY_ZERO);
    public string SecondSecondDigit = Convert.ToString(DateTime.Now.Second % 10, 2).PadLeft(4, BINARY_ZERO);
}
