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
            Assert.Contains(holidayMock.Object, holidayList.HolidaysList);
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
            Assert.Contains(holidayMock.Object, holidayList.HolidaysList);
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
            Assert.Contains(holidayMock.Object, holidayList.HolidaysList);
            Assert.Equal(result1, result2);
            Assert.Contains(result2, holidayList.HolidaysList);
        }

        [Fact]
        public void GetListHolidayMoreDays_ReturnsHolidaysWithMoreThanXDaysOff()
        {
            // Arrange
            var mockHoliday1 = new Mock<IHoliday>();
            mockHoliday1.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(2);
        
            var mockHoliday2 = new Mock<IHoliday>();
            mockHoliday2.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(0);

            var mockHoliday3 = new Mock<IHoliday>();
            mockHoliday3.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(3);
        
            var holidayList = new List<IHoliday> { mockHoliday1.Object, mockHoliday2.Object, mockHoliday3.Object };
            var numberOfDays = 1;
        
            // Act
            var result = holidayList.Where(h => h.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays) > 0).ToList();
        
            // Assert
            Assert.Equal(2, result.Count);
        }
    }
}