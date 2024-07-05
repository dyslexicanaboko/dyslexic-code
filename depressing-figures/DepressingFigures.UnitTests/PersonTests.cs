using DepressingFigures.Lib;
using NUnit.Framework;
using C = DepressingFigures.Lib.Constants;

namespace DepressingFigures.UnitTests
{
    [TestFixture]
    public class PersonTests
    {
        //These are assumptions for the tests only, they cannot be used generically
        private const int SleepHoursPerNight = 8;
        private const int WorkHoursPerWeek = 40;
        private const double OneYearOfAwakeHours = C.DaysPerYear * (C.HoursPerDay - SleepHoursPerNight);
        private const double OneYearOfSleepHours = C.DaysPerYear * SleepHoursPerNight;
        private const double OneYearOfWorkHours = C.WeeksPerYear * WorkHoursPerWeek;

        [Test]
        public void Expected_max_age_overrides_default()
        {
            //Arrange
            var svc = new LifeSpanService();
            var profile = GetDefaultProfile();

            //Act
            var estimates = svc.Calculate(profile);

            //Assert
            Assert.AreEqual(profile.ExpectedMaxAgeYears, estimates.Person.MaxYears);
        }

        [Test]
        public void One_year_span_has_no_time_remaining()
        {
            //Arrange
            var svc = new LifeSpanService();
            var profile = GetDefaultProfile();

            //Act
            var estimates = svc.Calculate(profile);

            //Assert
            Assert.AreEqual(1d, estimates.Years.Spent);
            Assert.AreEqual(0d, estimates.Years.Remaining);
            Assert.AreEqual(1d, estimates.Years.PercentSpent);
            Assert.AreEqual(0d, estimates.Years.PercentRemaining);

            Assert.AreEqual(C.SecondsPerYear, estimates.Seconds.Spent);
            Assert.AreEqual(0d, estimates.Seconds.Remaining);
            Assert.AreEqual(1d, estimates.Seconds.PercentSpent);
            Assert.AreEqual(0d, estimates.Seconds.PercentRemaining);
        }

        [Test]
        public void One_year_span_awake_hours()
        {
            //Arrange
            var svc = new LifeSpanService();
            var profile = GetDefaultProfile();

            //Act
            var estimates = svc.Calculate(profile);

            //Assert
            Assert.AreEqual(OneYearOfAwakeHours, estimates.AwakeHours.Spent);
            Assert.AreEqual(0d, estimates.AwakeHours.Remaining);
            Assert.AreEqual(1d, estimates.AwakeHours.PercentSpent);
            Assert.AreEqual(0d, estimates.AwakeHours.PercentRemaining);
        }

        [Test]
        public void One_year_span_sleep_hours()
        {
            //Arrange
            var svc = new LifeSpanService();
            var profile = GetDefaultProfile();

            //Act
            var estimates = svc.Calculate(profile);

            //Assert
            Assert.AreEqual(OneYearOfSleepHours, estimates.SleepHours.Spent);
            Assert.AreEqual(0d, estimates.SleepHours.Remaining);
            Assert.AreEqual(1d, estimates.SleepHours.PercentSpent);
            Assert.AreEqual(0d, estimates.SleepHours.PercentRemaining);
        }

        [Test]
        public void One_year_span_work_hours()
        {
            //Arrange
            var svc = new LifeSpanService();
            var profile = GetDefaultProfile();

            //Act
            var estimates = svc.Calculate(profile);

            //Assert
            //Weeks are a funny construct in comparison to days so there will never be a an exact comparison.
            //The magic number of total hours worked in a year is (52w * 40hpw) = 2,080h
            //Going to allow a margin of error (tolerance) of 6 points otherwise this will never pass
            Assert.AreEqual(OneYearOfWorkHours, estimates.WorkingHours.Spent, 6d);
            Assert.AreEqual(0d, estimates.WorkingHours.Remaining, 6d);
            Assert.AreEqual(1d, estimates.WorkingHours.PercentSpent, 6d);
            Assert.AreEqual(0d, estimates.WorkingHours.PercentRemaining, 6d);
        }

        private Profile GetDefaultProfile()
        {
            var profile = new Profile
            {
                BiologicalGender = Gender.Male,
                BirthDate = DateTime.Today.AddYears(-1), //Exactly one year
                ExpectedMaxAgeYears = 1,
                IsEmployed = true,
                SleepHoursPerNight = SleepHoursPerNight,
                WorkHoursPerWeek = WorkHoursPerWeek
            };

            return profile;
        }
    }
}