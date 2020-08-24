using System;
using System.Linq;
using System.Text;

namespace StringDurationUtils
{
    public static partial class StringDuration
    {
        public static string ToDurationString(this TimeSpan timeSpan)
        {
            return ToDurationString((long)timeSpan.TotalSeconds);
        }

        public static string ToDurationString(long seconds)
        {
            return ToDurationString(seconds, (long)DurationVariant.DAY, (long)DurationVariant.WEEK);
        }

        public static string ToDurationStringWithNegative(this TimeSpan timeSpan)
        {
            return ToDurationStringWithNegative((long)timeSpan.TotalSeconds);
        }

        public static string ToDurationStringWithNegative(long seconds)
        {
            if (seconds < 0)
            {
                return "-" + ToDurationString(-seconds);
            }
            else
            {
                return ToDurationString(seconds);
            }
        }

        public static string ToDurationString(long l, int hoursPerDay, int daysPerWeek)
        {
            long secondsInDay = hoursPerDay * (long)DurationVariant.HOUR;
            long secondsPerWeek = daysPerWeek * secondsInDay;
            return ToDurationString(l, secondsInDay, secondsPerWeek);
        }

        public static string ToDurationString(TimeSpan totalTimeSpan, TimeSpan timeSpanPerDay, TimeSpan timeSpanPerWeek)
        {
            return ToDurationString((long)totalTimeSpan.TotalSeconds, (long)timeSpanPerDay.TotalSeconds, (long)timeSpanPerWeek.TotalSeconds);
        }

        public static string ToDurationString(long l, long secondsPerDay, long secondsPerWeek)
        {
            if (l == 0)
            {
                return "0m";
            }

            StringBuilder result = new StringBuilder();

            if (l >= secondsPerWeek)
            {
                result.Append(l / secondsPerWeek);
                result.Append("w ");
                l = l % secondsPerWeek;
            }

            if (l >= secondsPerDay)
            {
                result.Append(l / secondsPerDay);
                result.Append("d ");
                l = l % secondsPerDay;
            }

            if (l >= (long)DurationVariant.HOUR)
            {
                result.Append(l / (long)DurationVariant.HOUR);
                result.Append("h ");
                l = l % (long)DurationVariant.HOUR;
            }

            if (l >= (long)DurationVariant.MINUTE)
            {
                result.Append(l / (long)DurationVariant.MINUTE);
                result.Append("m ");
            }

            return result.ToString().Trim();
        }
    }
}
