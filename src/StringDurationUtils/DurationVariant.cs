using System;
using System.Linq;

namespace StringDurationUtils
{
    public enum DurationVariant
    {
        SECOND = 1,
        MINUTE = 60 * DurationVariant.SECOND,
        HOUR = 60 * DurationVariant.MINUTE,
        DAY = 24 * DurationVariant.HOUR,
        WEEK = 7 * DurationVariant.DAY,
        MONTH = 31 * DurationVariant.DAY,
        YEAR = 52 * DurationVariant.WEEK
    }
}
