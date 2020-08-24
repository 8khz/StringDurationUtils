using NUnit.Framework;
using StringDurationUtils;
using System;

namespace StringDurationUtils.Test
{
   
    public class StringDurationTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckValidAndInvalidTimeDuration()
        {
            Assert.IsTrue(StringDuration.IsValidDuration("1h"));
            Assert.IsTrue(StringDuration.IsValidDuration("1d"));
            Assert.IsTrue(StringDuration.IsValidDuration("1m"));
            Assert.IsTrue(StringDuration.IsValidDuration("1h 1m"));
            Assert.IsTrue(StringDuration.IsValidDuration("1d 1h 1m"));
            Assert.IsTrue(StringDuration.IsValidDuration("1m 1h 1m"));
            Assert.IsTrue(StringDuration.IsValidDuration(" 1m 1h 1m "));
            Assert.IsTrue(StringDuration.IsValidDuration("-1w 2d 1m 1h 1m "));


            Assert.IsFalse(StringDuration.IsValidDuration("1xas"));
            Assert.IsFalse(StringDuration.IsValidDuration("2x 2b 2z"));
            Assert.IsFalse(StringDuration.IsValidDuration("qw1e23w 5e4 qw56e456qw1 "));
        }

        [Test]
        public void ConversionToStringIsValid()
        {
            // 5 days
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromDays(5)), "5d");

            // 1 week
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromDays(7)), "1w");

            // 5 minutes
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromMinutes(5)), "5m");

            // 10 days - 1 week and 3 days
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromDays(10)), "1w 3d");

            // 4 work weeks - 8 hours a day, 5 days a week, 4 weeks
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromHours(5 * 8 * 4), TimeSpan.FromHours(8), TimeSpan.FromHours(5 * 8)), "4w");

            // 11 workdays ( 8 hours a day, 5 days a week, two weeks + 1 work day) - 2w 1d
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromHours((8 * 5 * 2) + 8), TimeSpan.FromHours(8), TimeSpan.FromHours(5 * 8)), "2w 1d");

            // 10 workdays = 2w
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromHours(10 * 8), TimeSpan.FromHours(8), TimeSpan.FromHours(5 * 8)), "2w");

            // 1 workday
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromHours(8), TimeSpan.FromHours(8), TimeSpan.FromHours(5 * 8)), "1d");

            // 1 hour
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromMinutes(60), TimeSpan.FromHours(8), TimeSpan.FromHours(5 * 8)), "1h");

            // 25 minutes
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromMinutes(25), TimeSpan.FromHours(8), TimeSpan.FromHours(5 * 8)), "25m");

            // 1 hour and 5 minutes
            Assert.AreEqual(StringDuration.ToDurationString(TimeSpan.FromMinutes(65), TimeSpan.FromHours(8), TimeSpan.FromHours(5 * 8)), "1h 5m");
        }

        [Test]
        public void ConversionFromStringToTimeSpanIsValid()
        {
            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("1"), TimeSpan.FromMinutes(1));

            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("1m"), TimeSpan.FromMinutes(1));

            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("1m 1m"), TimeSpan.FromMinutes(2));

            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("1h"), TimeSpan.FromHours(1));

            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("1d"), TimeSpan.FromHours(24));

            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("1w"), TimeSpan.FromDays(7));

            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("4w"), TimeSpan.FromDays(28));

            Assert.AreEqual(StringDuration.GetDurationInTimeSpan("4w 3h 2m"), TimeSpan.FromMinutes((4 * 7 * 1440) + 180 + 2));
        }

        [Test]
        public void ConversionFromStringToSecondsIsValid()
        {
            Assert.AreEqual(StringDuration.GetDurationInSeconds("-1m"), -60);
            Assert.AreEqual(StringDuration.GetDurationInSeconds("1m"), 60);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1m 1m"), TimeSpan.FromMinutes(2).TotalSeconds);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1h"), TimeSpan.FromHours(1).TotalSeconds);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1d"), TimeSpan.FromDays(1).TotalSeconds);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1w"), TimeSpan.FromDays(7).TotalSeconds);
            
            Assert.AreEqual(StringDuration.GetDurationInSeconds("4w"), TimeSpan.FromDays(7 * 4).TotalSeconds);

            Assert.AreEqual(
                StringDuration.GetDurationInSeconds("4w 3h 2m"),
                TimeSpan.FromDays(7 * 4).TotalSeconds + TimeSpan.FromHours(3).TotalSeconds + TimeSpan.FromMinutes(2).TotalSeconds);
        }

        [Test]
        public void ConversionFromStringToModifiedSecondsIsValid()
        {
            long secondsPerDay = 60 * 60 * 8;
            long secondsPerWeek = secondsPerDay * 5;

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1m", secondsPerDay, secondsPerWeek, DurationVariant.MINUTE), 60);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1m 1m", secondsPerDay, secondsPerWeek, DurationVariant.MINUTE), 120);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1h", secondsPerDay, secondsPerWeek, DurationVariant.MINUTE), 60 * 60);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1d", secondsPerDay, secondsPerWeek, DurationVariant.MINUTE), secondsPerDay);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("1w", secondsPerDay, secondsPerWeek, DurationVariant.MINUTE), secondsPerWeek);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("4w", secondsPerDay, secondsPerWeek, DurationVariant.MINUTE), secondsPerWeek * 4);

            Assert.AreEqual(StringDuration.GetDurationInSeconds("4w 3h 2m", secondsPerDay, secondsPerWeek, DurationVariant.MINUTE), (secondsPerWeek * 4) + (60 * 60 * 3) + 120);
        }
    }
}