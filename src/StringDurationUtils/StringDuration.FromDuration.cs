using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringDurationUtils
{

    public static partial class StringDuration
    {
        private static Regex DURATION_PATTERN = new Regex("(\\d+(?:\\.\\d+)?|\\.\\d+)(.+)");

        public static bool TryParse(string duration, out long timeSpan)
        {
            bool success;

            try
            {
                timeSpan = GetDurationInSeconds(duration);
                success = true;
            }
            catch
            {
                timeSpan = default(long);
                success = false;
            }

            return success;
        }

        public static TimeSpan GetDurationInTimeSpan(string durationStr)
        {
            return TimeSpan.FromSeconds(GetDurationInSeconds(durationStr));
        }

        public static long GetDurationInSeconds(string durationStr)
        {
            return GetDurationInSeconds(
                durationStr, 
                (int)DurationVariant.DAY, 
                (int)DurationVariant.WEEK, 
                DurationVariant.MINUTE);
        }

        public static TimeSpan GetDurationInTimeSpan(
            string durationStr,
            TimeSpan dayTimeSpan,
            TimeSpan weekTimeSpan, 
            DurationVariant defaultUnit)
        {
            return TimeSpan.FromSeconds(
                GetDurationInSeconds(
                    durationStr, 
                    (long)dayTimeSpan.TotalSeconds, 
                    (long)weekTimeSpan.TotalSeconds, 
                    defaultUnit));
        }

        public static long GetDurationInSeconds(
            string durationStr, 
            long secondsPerDay, 
            long secondsPerWeek, 
            DurationVariant defaultUnit)
        {
            long totalSeconds = 0;

            if (durationStr == null || string.IsNullOrWhiteSpace(durationStr.Trim()))
            {
                return 0;
            }

            durationStr = durationStr.Trim().ToLower();

            int multiplier = durationStr.StartsWith("-") ? -1 : 1;

            string[] st = durationStr.Split(' ');

            foreach (string word in st)
            {
                totalSeconds += GetSecondsPerDuration(word, secondsPerDay, secondsPerWeek, defaultUnit);
            }

            return multiplier * totalSeconds;
        }

        private static long GetSecondsPerDuration(
            string duration, 
            long secondsPerDay, 
            long secondsPerWeek, 
            DurationVariant defaultUnit)
        {
            long timePerUnit = 0;

            try
            {
                timePerUnit = long.Parse(duration.Trim()) * defaultUnit.GetSeconds(secondsPerDay, secondsPerWeek);
            }
            catch (Exception)
            {
                Match m = DURATION_PATTERN.Match(duration);

                if (m.Success)
                {
                    string numberAsString = m.Groups[1].Value;
                    int number = int.Parse(numberAsString);

                    string durationUnitAsString = m.Groups[2].Value;

                    long unit = GetSecondsFromDurationUnit(durationUnitAsString, secondsPerDay, secondsPerWeek);
                    long seconds = number * unit;

                    timePerUnit = seconds;
                }
                else
                {
                    throw new Exception("Unable to parse duration string: " + duration);
                }
            }

            return timePerUnit;
        }
    }
}
