
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Domain.Tests
{
    public class HolidaysFactoryTest
    {
        // public Holidays NewHoliday(IHolidayFactory hFactory)
        [Fact]
        public void IfPassingValidDates_ShouldReturnANewHolidays()
        {
            // Arrange
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            HolidaysFactory hFactory = new HolidaysFactory();
            // act
            var result = hFactory.NewHoliday(holidayFactoryMock.Object);
            // assert
            Assert.IsType<Holidays>(result);
        }
    }
}