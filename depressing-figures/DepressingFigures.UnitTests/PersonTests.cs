using DepressingFigures.Lib;
using NUnit.Framework;

namespace DepressingFigures.UnitTests
{
    [TestFixture]
    public class PersonTests
    {
        [Test]
        public void Expected_max_age_overrides_default()
        {
            //Arrange
            var svc = new LifeSpanService();
            var profile = new Profile
            {
                BiologicalGender = Gender.Male,
                BirthDate = new DateTime(1921, 01, 01),
                ExpectedMaxAgeYears = 150,
                IsEmployed = true,
                SleepHoursPerNight = 8,
                WorkHoursPerWeek = 40
            };
            
            //Act
            var estimates = svc.Calculate(profile);

            //Assert
            Assert.AreEqual(profile.ExpectedMaxAgeYears, estimates.Person.MaxAgeYears);
        }
    }
}