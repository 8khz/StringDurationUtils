using System;

namespace StringDurationUtils
{
    public partial class StringDuration
    {

        public static bool IsValidDuration(string s)
        {
            try
            {
                GetDurationInSeconds(s);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static DurationVariant GetDurationVariant(string unit)
        {
            DurationVariant durationvariant;

            switch (unit[0])
            {
                case 'm':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.MINUTE);
                    durationvariant = DurationVariant.MINUTE;
                    break;
                case 'h':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.HOUR);
                    durationvariant = DurationVariant.HOUR;
                    break;
                case 'd':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.DAY);
                    durationvariant = DurationVariant.DAY;
                    break;
                case 'w':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.WEEK);
                    durationvariant = DurationVariant.WEEK;
                    break;
                default:
                    throw new Exception("Not a valid duration string");
            }

            return durationvariant;
        }


        public static long GetSecondsFromDurationUnit(string unit, long secondsPerDay, long secondsPerWeek)
        {
            long time;
            switch ((int)unit[0])
            {
                case 'm':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.MINUTE);
                    time = (int)DurationVariant.MINUTE;
                    break;
                case 'h':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.HOUR);
                    time = (int)DurationVariant.HOUR;
                    break;
                case 'd':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.DAY);
                    time = secondsPerDay;
                    break;
                case 'w':
                    ValidateDurationUnit(unit.Substring(0), DurationVariant.WEEK);
                    time = secondsPerWeek;
                    break;
                default:
                    throw new Exception("Not a valid duration string");
            }
            return time;
        }

        public static string ValidateDurationUnit(string durationString, DurationVariant duration)
        {
            if (durationString.Length > 1)
            {
                string singular = duration.ToString().ToLower();
                string plural = duration.ToString().ToLower() + "s";

                if (durationString.Contains(plural))
                {
                    return durationString.Substring(durationString.IndexOf(plural));
                }
                else if (durationString.Contains(singular))
                {
                    return durationString.Substring(durationString.IndexOf(singular));
                }
                else
                {
                    throw new Exception("Not a valid durationString string");
                }
            }

            return durationString.Substring(1);
        }
    }
}
