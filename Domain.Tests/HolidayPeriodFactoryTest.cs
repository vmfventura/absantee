
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Domain.Tests
{
    public class HolidayPeriodFactoryTest
    {
        [Fact]
        public void IfPassingValidDates_ShouldReturnANewHolidayPeriod()
        {
            // Arrange
            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 5);
            HolidayPeriodFactory hpFactory = new HolidayPeriodFactory();
            // act
            var result = hpFactory.NewHolidayPeriod(startDate, endDate);
            // assert
            Assert.IsType<HolidayPeriod>(result);
        }
    }
}