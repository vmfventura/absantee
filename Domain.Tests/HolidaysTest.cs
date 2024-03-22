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
            var ex = Assert.Throws<ArgumentException>(() => new Holidays(null));
        
            Assert.Equal("Holiday Factory cannot be null", ex.Message);
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

        [Theory]
        [InlineData(2)]
        [InlineData(0)]
        public void GetListHolidayMoreDays_ReturnsZero(int numberOfDays)
        {
            // Arrange

            // var numberOfDays = 2;
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Holidays holidays = new Holidays(holidayFactoryMock.Object);

            // Act
            var result = holidays.GetListHolidayMoreDays(numberOfDays).ToList();
        
            // Assert
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void GetListHolidayMoreDays_ReturnsHolidayListWithMoreThanXDaysOff()
        {
            // Arrange

            var numberOfDays = 20;

            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Mock<IHoliday> holidayDouble = new Mock<IHoliday>();

            holidayDouble.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(true);

            holidayFactoryMock.Setup(hF => hF.NewHoliday(colabDouble.Object)).Returns(holidayDouble.Object);


            Holidays holidays = new Holidays(holidayFactoryMock.Object);

            IHoliday holidayResult = holidays.addHoliday(colabDouble.Object);

            // Act
            var result = holidays.GetListHolidayMoreDays(numberOfDays).ToList();
        
            // Assert
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void GetListHolidayFilterByColaborator_ReturnsHolidayListFilterByDateRange()
        {
            // Arrange
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();

            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 31);
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Mock<IHoliday> holidayDouble = new Mock<IHoliday>();

            holidayDouble.Setup(h => h.hasColaboratorAndHolidayPeriodsDuring(colabDouble.Object, startDate, endDate)).Returns(true);

            holidayFactoryMock.Setup(hF => hF.NewHoliday(colabDouble.Object)).Returns(holidayDouble.Object);

            Holidays holidays = new Holidays(holidayFactoryMock.Object);

            IHoliday holidayResult = holidays.addHoliday(colabDouble.Object);

            // Act
            var result = holidays.getListHolidayFilterByColaborator(colabDouble.Object, startDate, endDate).ToList();
        
            // Assert
            Assert.Equal(1, result.Count);
        }

        [Fact]
        public void GetListHolidayFilterByColaborator_ReturnsArgumentException()
        {
            // Arrange
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();

            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 31);
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Mock<IHoliday> holidayDouble = new Mock<IHoliday>();

            holidayDouble.Setup(h => h.hasColaboratorAndHolidayPeriodsDuring(colabDouble.Object, startDate, endDate)).Returns(false);

            holidayFactoryMock.Setup(hF => hF.NewHoliday(colabDouble.Object)).Returns(holidayDouble.Object);

            Holidays holidays = new Holidays(holidayFactoryMock.Object);

            IHoliday holidayResult = holidays.addHoliday(colabDouble.Object);

            // Act

            var ex = Assert.Throws<ArgumentException>(() => holidays.getListHolidayFilterByColaborator(colabDouble.Object, startDate, endDate));
        
            // Assert
            Assert.Equal("No holiday found for this colaborator", ex.Message);
        }

        [Fact]
        public void WhenPassingColaborator_ShouldReturnTotalDaysOff()
        {
            // arrange

            Mock<IColaborator> colabDouble = new Mock<IColaborator>();

            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 31);
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Mock<IHoliday> holidayDouble = new Mock<IHoliday>();

            holidayDouble.Setup(h => h.getNumberOfHolidayPeriodsDays()).Returns(10);
            holidayDouble.Setup(h => h.hasColaborador(colabDouble.Object)).Returns(true);

            holidayFactoryMock.Setup(hF => hF.NewHoliday(colabDouble.Object)).Returns(holidayDouble.Object);

            Holidays holidays = new Holidays(holidayFactoryMock.Object);

            IHoliday holidayResult = holidays.addHoliday(colabDouble.Object);

            // act
            var result = holidays.getNumberOfDaysByColaborator(colabDouble.Object);

            // assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void WhenPassingColaborator_ShouldReturnSumDaysOff()
        {
            // arrange
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        
            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 31);
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Mock<IHoliday> holidayDouble1 = new Mock<IHoliday>();
            Mock<IHoliday> holidayDouble2 = new Mock<IHoliday>();
        
            holidayDouble1.Setup(h => h.getNumberOfHolidayPeriodsDays()).Returns(20);
            holidayDouble2.Setup(h => h.getNumberOfHolidayPeriodsDays()).Returns(20);
        
            holidayDouble1.Setup(h => h.hasColaborador(colabDouble.Object)).Returns(true);
            holidayDouble2.Setup(h => h.hasColaborador(colabDouble.Object)).Returns(true);
        
            holidayFactoryMock.SetupSequence(hF => hF.NewHoliday(colabDouble.Object))
                               .Returns(holidayDouble1.Object)
                               .Returns(holidayDouble2.Object);
        
            Holidays holidays = new Holidays(holidayFactoryMock.Object);
        
            IHoliday holidayResult1 = holidays.addHoliday(colabDouble.Object);
            IHoliday holidayResult2 = holidays.addHoliday(colabDouble.Object);
        
            // act
            var sumResult = holidays.getNumberOfDaysByColaborator(colabDouble.Object);
        
            // assert
            Assert.Equal(40, sumResult);
        }

        [Fact]
        public void WhenPassingColaboratorAndDates_ShouldReturnTotalDaysOff()
        {
            // arrange

            Mock<IColaborator> colabDouble = new Mock<IColaborator>();

            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 31);
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Mock<IHoliday> holidayDouble = new Mock<IHoliday>();

            holidayDouble.Setup(h => h.hasColaboratorAndHolidayPeriodsDuring(colabDouble.Object, startDate, endDate)).Returns(true);
            holidayDouble.Setup(h => h.getNumberOfHolidayPeriodsDays()).Returns(30);

            holidayFactoryMock.Setup(hF => hF.NewHoliday(colabDouble.Object)).Returns(holidayDouble.Object);

            Holidays holidays = new Holidays(holidayFactoryMock.Object);

            IHoliday holidayResult = holidays.addHoliday(colabDouble.Object);

            // act
            var result = holidays.getNumberOfHolidaysDaysForColaboratorDuringPeriod(colabDouble.Object, startDate, endDate);

            // assert
            Assert.Equal(30, result);
        }

        [Fact]
        public void WhenPassingColaboratorAndDates_ShouldReturnSumDaysOff()
        {
            // arrange
            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        
            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 31);
        
            Mock<IHolidayFactory> holidayFactoryMock = new Mock<IHolidayFactory>();
            Mock<IHoliday> holidayDouble1 = new Mock<IHoliday>();
            Mock<IHoliday> holidayDouble2 = new Mock<IHoliday>();
        
            holidayDouble1.Setup(h => h.hasColaboratorAndHolidayPeriodsDuring(colabDouble.Object, startDate, endDate)).Returns(true);
            holidayDouble1.Setup(h => h.getNumberOfHolidayPeriodsDays()).Returns(20);
            holidayDouble2.Setup(h => h.hasColaboratorAndHolidayPeriodsDuring(colabDouble.Object, startDate, endDate)).Returns(true);
            holidayDouble2.Setup(h => h.getNumberOfHolidayPeriodsDays()).Returns(20);
        
            holidayFactoryMock.SetupSequence(hF => hF.NewHoliday(colabDouble.Object))
                               .Returns(holidayDouble1.Object)
                               .Returns(holidayDouble2.Object);
        
            Holidays holidays = new Holidays(holidayFactoryMock.Object);
        
            IHoliday holidayResult1 = holidays.addHoliday(colabDouble.Object);
            IHoliday holidayResult2 = holidays.addHoliday(colabDouble.Object);
        
            // act
            var result = holidays.getNumberOfHolidaysDaysForColaboratorDuringPeriod(colabDouble.Object, startDate, endDate);
        
            // assert
            Assert.Equal(40, result);
        }

    }
}