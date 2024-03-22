
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Domain.Tests
{
    public class HolidayFactoryTest
    {
        // public Holiday NewHoliday(IColaborator colaborator)
        [Fact]
        public void IfPassingValidDates_ShouldReturnANewHoliday()
        {
            // arrange
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
            HolidayFactory holiday = new HolidayFactory();
            // act
            var result = holiday.NewHoliday(colabDouble.Object);
            // assert
            Assert.IsType<Holiday>(result);
        }
    }
}