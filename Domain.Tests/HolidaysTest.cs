using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public class HolidaysTest
    {

        // public Holidays(List<Holiday> holidayList)

        [Fact]
        public void WhenPassingCorrectData_ThenHolidaysShouldBeInstantiated()
        {
            // arrange
            Mock<List<Holiday>> _holidayDouble = new Mock<List<Holiday>>();

            // act

            new Holidays (_holidayDouble.Object);
        }


        [Fact]
        public void WhenPassingNullAsHolidayList_ThenThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Holidays(null));
        }

        [Fact]
        public void WhenAddHoliday_ShouldReturnHolidaysList()
        {
            // arrange
            Mock<List<Holiday>> _holidayDouble = new Mock<List<Holiday>>();
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
            Mock<IHolidayFactory> hFactoryDouble = new Mock<IHolidayFactory>();

            Holidays holidays = new Holidays(_holidayDouble.Object);

            hFactoryDouble.Setup(hF => hF.NewHoliday(colabDouble.Object)).Returns(new Holiday(colabDouble.Object));

            // act
            Holiday holiday = holidays.addHoliday(hFactoryDouble.Object, colabDouble.Object);

            // arrange
            Assert.Equivalent(, holiday)
        }

    }
}