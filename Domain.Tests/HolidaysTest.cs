using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public class HolidaysTest
    {

        [Fact]
        public void WhenPassingCorrectData_ThenHolidaysShouldBeInstantiated()
        {
            // arrange
            Mock<IHolidayFactory> _holidayDouble = new Mock<IHolidayFactory>();

            // act

            new Holidays(_holidayDouble.Object);
        }


        [Fact]
        public void WhenPassingNullAsHolidayList_ThenThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new Holidays(null));
        }

        [Fact]
        public void AddHoliday_ShouldAddHolidayToHolidayList()
        {
            // Arrange
            var holidayFactoryMock = new Mock<IHolidayFactory>();
            var colaboratorMock = new Mock<IColaborator>();
            var holidayMock = new Mock<Holiday>(colaboratorMock.Object);
            holidayFactoryMock.Setup(x => x.NewHoliday(colaboratorMock.Object)).Returns(holidayMock.Object);
            var holidayList = new Holidays(holidayFactoryMock.Object);

            // Act
            var result = holidayList.addHoliday(colaboratorMock.Object);

            // Assert
            Assert.Equal(holidayMock.Object, result);
        }

        [Fact]
        public void AddHoliday_ShouldAddNullHolidayToHolidayList()
        {
            // Arrange
            var holidayFactoryMock = new Mock<IHolidayFactory>();
            var colaboratorMock = new Mock<IColaborator>();
            var holidayMock = new Mock<Holiday>(colaboratorMock.Object);
            holidayFactoryMock.Setup(x => x.NewHoliday(null)).Returns(holidayMock.Object);
            var holidayList = new Holidays(holidayFactoryMock.Object);

            // Act
            var result = holidayList.addHoliday(null);

            // Assert
            Assert.Equal(holidayMock.Object, result);
        }

        [Fact]
        public void AddHoliday_ShouldAddHolidayToHolidayListMultipleTimes()
        {
            // Arrange
            var holidayFactoryMock = new Mock<IHolidayFactory>();
            var colaboratorMock = new Mock<IColaborator>();
            var holidayMock = new Mock<Holiday>(colaboratorMock.Object);
            holidayFactoryMock.Setup(x => x.NewHoliday(colaboratorMock.Object)).Returns(holidayMock.Object);
            var holidayList = new Holidays(holidayFactoryMock.Object);

            // Act
            var result1 = holidayList.addHoliday(colaboratorMock.Object);
            var result2 = holidayList.addHoliday(colaboratorMock.Object);

            // Assert
            Assert.Equal(holidayMock.Object, result1);
            Assert.Equal(result1, result2);
        }

        [Fact]
        public void GetListHolidayMoreDays_ReturnsHolidayListWithMoreThanXDaysOff()
        {
            // Arrange
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Holidays holidays = new Holidays(holidayFactoryMock.Object);
            
            var mockHoliday = new List<Holiday>() {};
            var mockHolidays1 = new Mock<IHolidays>();
            mockHolidays1.Setup(h => h.GetListHolidayMoreDays(It.IsAny<int>())).Returns(mockHoliday);
        
            // var holidayList = new List<Holiday> { mockHoliday };
            var numberOfDays = 2;
        
            // Act
            var result = holidays.GetListHolidayMoreDays(numberOfDays).ToList();
        
            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}