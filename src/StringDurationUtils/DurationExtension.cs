namespace StringDurationUtils
{
    public static class DurationExtension
    {
        public static long GetSeconds(this DurationVariant duration, long secondsPerDay, long secondsPerWeek)
        {
            switch (duration)
            {
                case DurationVariant.SECOND:
                case DurationVariant.MINUTE:
                case DurationVariant.HOUR:
                    return (long)duration;
                case DurationVariant.DAY:
                    return secondsPerDay;
                case DurationVariant.WEEK:
                    return secondsPerWeek;
                case DurationVariant.MONTH:
                    return 31 * secondsPerDay;
                case DurationVariant.YEAR:
                    return 52 * secondsPerWeek;
                default:
                    return (long)duration;
            }
        }
    }
}
