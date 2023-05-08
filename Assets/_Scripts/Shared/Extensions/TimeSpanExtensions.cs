using System;

public static class TimeSpanExtensions
{
    public static TimeSpan sec(this double value) =>
        TimeSpan.FromSeconds(value);
    public static TimeSpan sec(this float value) =>
        ((double)value).sec();
    public static TimeSpan sec(this int value) =>
        ((double)value).sec();
}