using System.IO.Compression;
using Xunit.Sdk;

namespace Domain.Tests;

public class HolidayTest
{

    [Fact]
    public void WhenPassingAColaborator_ThenHolidayIsInstantiated()
    {
        //Mock<Colaborator> colabDouble = new Mock<Colaborator>("a", "b@b.pt");
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        new Holiday(colabDouble.Object);

        // isto não é um tewste unitário a Holiday, porque não isola do Colaborator
        // Colaborator colab = new Colaborator("a", "a@b.c");
        // IColaborator colab = new Colaborator("a", "a@b.c");
        // new Holiday(colab);
    }

    [Fact]
    public void WhenPassingNullAsColaborator_ThenThrowsException()
    {
        var ex = Assert.Throws<ArgumentException>(() => new Holiday(null));

        Assert.Equal("Invalid argument: colaborator must be non null", ex.Message);
    }

    [Fact]
    public void WhenRequestingName_ThenReturnColaboratorName()
    {
        // arrange
        string NOME = "nome";
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        colabDouble.Setup(p => p.getName()).Returns(NOME);

        Holiday holiday = new Holiday(colabDouble.Object); // SUT/OUT

        // act
        string nameResult = holiday.getName();

        // assert
        Assert.Equal(NOME, nameResult);
    }

    [Fact]
    public void WhenRequestingCorrectHasColaborator_ShouldReturnTrue()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Holiday holiday = new Holiday(colabDouble.Object);

        // act
        bool hasColab = holiday.hasColaborador(colabDouble.Object);

        // assert
        Assert.True(hasColab);
    }

    [Fact]
    public void WhenRequestingWrongHasColaborator_ShouldReturnFalse()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IColaborator> colabDouble2 = new Mock<IColaborator>();
        Holiday holiday = new Holiday(colabDouble.Object);

        // act
        bool hasColab = holiday.hasColaborador(colabDouble2.Object);

        // assert
        Assert.False(hasColab);
    }


    [Fact]
    public void WhenRequestingHolidayPeriods_ShouldReturnHolidayPeriods()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble = new Mock<IHolidayPeriodFactory>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble2 = new Mock<IHolidayPeriodFactory>();
        
        var holiday = new Holiday(colabDouble.Object);        

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 12, 31);

        DateOnly startDateFirstHoliday = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDateFirstHoliday=  new DateOnly(DateTime.Now.Year, 03, 20);
        DateOnly startDateSecondHoliday = new DateOnly(DateTime.Now.Year, 07, 31);
        DateOnly endDateSecondHoliday=  new DateOnly(DateTime.Now.Year, 08, 15);        


        Mock<HolidayPeriod> hpDouble1 = new Mock<HolidayPeriod>(startDateFirstHoliday, endDateFirstHoliday);
        Mock<HolidayPeriod> hpDouble2 = new Mock<HolidayPeriod>(startDateSecondHoliday, endDateSecondHoliday);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDateFirstHoliday, endDateFirstHoliday)).Returns(hpDouble1.Object);
        hpFactoryDouble2.Setup(hpF => hpF.NewHolidayPeriod(startDateSecondHoliday, endDateSecondHoliday)).Returns(hpDouble2.Object);

        HolidayPeriod holidayPeriod1 = holiday.addHolidayPeriod(hpFactoryDouble.Object, startDateFirstHoliday, endDateFirstHoliday);
        HolidayPeriod holidayPeriod2 = holiday.addHolidayPeriod(hpFactoryDouble2.Object, startDateSecondHoliday, endDateSecondHoliday);

        List<HolidayPeriod> holidayPeriods = new List<HolidayPeriod> { holidayPeriod1, holidayPeriod2};

        // act
        List<HolidayPeriod> result = holiday.getHolidayPeriodsDuring(startDate, endDate);

        // assert
        Assert.Equivalent(holidayPeriods, result);
    }

    public static readonly object[][] SucessCases =
    [
        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,12,01)]
    ];
    [Theory, MemberData(nameof(SucessCases))]
    public void WhenRequestingaddHolidayPeriod_ThenReturnHolidayPeriod(DateOnly startDate, DateOnly endDate)
    {

        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Holiday holidayDouble = new Holiday(colabDouble.Object);
        Mock<IHolidayPeriodFactory> hpFactoryDouble = new Mock<IHolidayPeriodFactory>();

        Mock<HolidayPeriod> holidayPeriodExpected = new Mock<HolidayPeriod>(startDate, endDate);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected.Object); // to isolate the result

        // act
        HolidayPeriod holidayPeriod = holidayDouble.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate); // to get the actual result

        // assert
        Assert.Equivalent(holidayPeriodExpected.Object, holidayPeriod); // compare objects
    }

    [Fact]
    public void TestGetHolidayPeriodsDuring_OutOfRange()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble = new Mock<IHolidayPeriodFactory>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble2 = new Mock<IHolidayPeriodFactory>();
        
        var holiday = new Holiday(colabDouble.Object);

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 01, 31);

        DateOnly startDateFirstHoliday = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDateFirstHoliday=  new DateOnly(DateTime.Now.Year, 03, 20);
        DateOnly startDateSecondHoliday = new DateOnly(DateTime.Now.Year, 07, 31);
        DateOnly endDateSecondHoliday=  new DateOnly(DateTime.Now.Year, 08, 15);        


        Mock<HolidayPeriod> hpDouble1 = new Mock<HolidayPeriod>(startDateFirstHoliday, endDateFirstHoliday);
        Mock<HolidayPeriod> hpDouble2 = new Mock<HolidayPeriod>(startDateSecondHoliday, endDateSecondHoliday);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDateFirstHoliday, endDateFirstHoliday)).Returns(hpDouble1.Object);
        hpFactoryDouble2.Setup(hpF => hpF.NewHolidayPeriod(startDateSecondHoliday, endDateSecondHoliday)).Returns(hpDouble2.Object);

        HolidayPeriod holidayPeriod1 = holiday.addHolidayPeriod(hpFactoryDouble.Object, startDateFirstHoliday, endDateFirstHoliday);
        HolidayPeriod holidayPeriod2 = holiday.addHolidayPeriod(hpFactoryDouble2.Object, startDateSecondHoliday, endDateSecondHoliday);

        List<HolidayPeriod> holidayPeriods = new List<HolidayPeriod> { };

        // act
        List<HolidayPeriod> result = holiday.getHolidayPeriodsDuring(startDate, endDate);

        // assert
        Assert.Equivalent(holidayPeriods, result);
    }

    [Fact]
    public void TestGetHolidayPeriodsDuring_EndDateEqualsStartDate()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble = new Mock<IHolidayPeriodFactory>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble2 = new Mock<IHolidayPeriodFactory>();
        
        var holiday = new Holiday(colabDouble.Object);

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 01, 31);

        DateOnly startDateFirstHoliday = new DateOnly(DateTime.Now.AddYears(-1).Year, 02, 01);
        DateOnly endDateFirstHoliday=  new DateOnly(DateTime.Now.Year, 01, 02);
        // DateOnly startDateSecondHoliday = new DateOnly(DateTime.Now.Year, 07, 31);
        // DateOnly endDateSecondHoliday=  new DateOnly(DateTime.Now.Year, 08, 15);        


        Mock<HolidayPeriod> hpDouble1 = new Mock<HolidayPeriod>(startDateFirstHoliday, endDateFirstHoliday);
        // Mock<HolidayPeriod> hpDouble2 = new Mock<HolidayPeriod>(startDateSecondHoliday, endDateSecondHoliday);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDateFirstHoliday, endDateFirstHoliday)).Returns(hpDouble1.Object);
        // hpFactoryDouble2.Setup(hpF => hpF.NewHolidayPeriod(startDateSecondHoliday, endDateSecondHoliday)).Returns(hpDouble2.Object);

        HolidayPeriod holidayPeriod1 = holiday.addHolidayPeriod(hpFactoryDouble.Object, startDateFirstHoliday, endDateFirstHoliday);
        // HolidayPeriod holidayPeriod2 = holiday.addHolidayPeriod(hpFactoryDouble2.Object, startDateSecondHoliday, endDateSecondHoliday);

        List<HolidayPeriod> holidayPeriods = new List<HolidayPeriod> { holidayPeriod1 };

        // act
        List<HolidayPeriod> result = holiday.getHolidayPeriodsDuring(startDate, endDate);

        // assert
        Assert.Equivalent(holidayPeriods, result);
    }

    [Fact]
    public void TestGetHolidayPeriodsDuring_StartDateEqualsEndDate()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble = new Mock<IHolidayPeriodFactory>();
        Mock<IHolidayPeriodFactory> hpFactoryDouble2 = new Mock<IHolidayPeriodFactory>();
        
        var holiday = new Holiday(colabDouble.Object);

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 01, 31);

        DateOnly startDateFirstHoliday = new DateOnly(DateTime.Now.Year, 01, 31);
        DateOnly endDateFirstHoliday=  new DateOnly(DateTime.Now.Year, 03, 20);
        DateOnly startDateSecondHoliday = new DateOnly(DateTime.Now.Year, 07, 31);
        DateOnly endDateSecondHoliday=  new DateOnly(DateTime.Now.Year, 08, 15);        


        Mock<HolidayPeriod> hpDouble1 = new Mock<HolidayPeriod>(startDateFirstHoliday, endDateFirstHoliday);
        Mock<HolidayPeriod> hpDouble2 = new Mock<HolidayPeriod>(startDateSecondHoliday, endDateSecondHoliday);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDateFirstHoliday, endDateFirstHoliday)).Returns(hpDouble1.Object);
        hpFactoryDouble2.Setup(hpF => hpF.NewHolidayPeriod(startDateSecondHoliday, endDateSecondHoliday)).Returns(hpDouble2.Object);

        HolidayPeriod holidayPeriod1 = holiday.addHolidayPeriod(hpFactoryDouble.Object, startDateFirstHoliday, endDateFirstHoliday);
        HolidayPeriod holidayPeriod2 = holiday.addHolidayPeriod(hpFactoryDouble2.Object, startDateSecondHoliday, endDateSecondHoliday);

        List<HolidayPeriod> holidayPeriods = new List<HolidayPeriod> { };

        // act
        List<HolidayPeriod> result = holiday.getHolidayPeriodsDuring(startDate, endDate);

        // assert
        Assert.Equivalent(holidayPeriods, result);
    }

    [Fact]
    public void GetListHolidayMoreDays_ReturnsHolidayListWithMoreThanXDaysOff()
    {
        // Arrange
        var mockHoliday1 = new Mock<IHoliday>();
        mockHoliday1.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(true);
    
        var mockHoliday2 = new Mock<IHoliday>();
        mockHoliday2.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(true);

        var mockHoliday3 = new Mock<IHoliday>();
        mockHoliday3.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(true);
    
        var holidayList = new List<IHoliday> { mockHoliday1.Object, mockHoliday2.Object, mockHoliday3.Object };
        var numberOfDays = 1;
    
        // Act
        var result = holidayList.Where(h => h.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays)).ToList();
    
        // Assert
        Assert.Equal(3, result.Count);
    }


    [Fact]
    public void GetListHolidayMoreDays_ReturnsEmptyHolidayList()
    {
        // Arrange
        var mockHoliday1 = new Mock<IHoliday>();
        mockHoliday1.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(false);
    
        var mockHoliday2 = new Mock<IHoliday>();
        mockHoliday2.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(false);

        var mockHoliday3 = new Mock<IHoliday>();
        mockHoliday3.Setup(h => h.getHolidaysDaysWithMoreThanXDaysOff(It.IsAny<int>())).Returns(false);
    
        var holidayList = new List<IHoliday> { mockHoliday1.Object, mockHoliday2.Object, mockHoliday3.Object };
        var numberOfDays = 20;
    
        // Act
        var result = holidayList.Where(h => h.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays)).ToList();
    
        // Assert
        Assert.Equal(0, result.Count);
    }

    [Fact]
    public void IfHaveMoreDaysOffThanX_ReturnsTrue()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Holiday holiday = new Holiday(colabDouble.Object);
        var hpFactoryDouble = new Mock<IHolidayPeriodFactory>();
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);
        HolidayPeriod holidayPeriodExpected = new HolidayPeriod(startDate, endDate);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected);// to isolate the result

        HolidayPeriod hp1 = holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate);

        // act
        
        bool result = holiday.getHolidaysDaysWithMoreThanXDaysOff(10);

        // assert
        Assert.True(result);
    }

    [Fact]
    public void IfHaveMinusDaysOffThanX_ReturnsFalse()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Holiday holiday = new Holiday(colabDouble.Object);
        var hpFactoryDouble = new Mock<IHolidayPeriodFactory>();
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 02);
        HolidayPeriod holidayPeriodExpected = new HolidayPeriod(startDate, endDate);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected);// to isolate the result

        HolidayPeriod hp1 = holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate);

        // act
        
        bool result = holiday.getHolidaysDaysWithMoreThanXDaysOff(8);

        // assert
        Assert.False(result);
    }

    [Fact]
    public void WhenPassingHolidayPeriod_GetNumberOfDays()
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        var hpFactoryDouble = new Mock<IHolidayPeriodFactory>();

        int expectedValue = 9;

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 10);

        HolidayPeriod holidayPeriodExpected = new HolidayPeriod(startDate, endDate);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected);// to isolate the result

        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate);

        // act
        int numberOfDaysResult = _holiday.getNumberOfHolidayPeriodsDays();

        // assert
        Assert.Equivalent(expectedValue, numberOfDaysResult);
    }

    [Fact]
    public void WhenPassingMultipleHolidayPeriods_GetNumberOfDays()
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        var hpFactoryDouble = new Mock<IHolidayPeriodFactory>();
        var hpFactoryDouble2 = new Mock<IHolidayPeriodFactory>();

        int expectedValue = 18;

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 10);
        DateOnly startDate2 = new DateOnly(DateTime.Now.Year, 03, 01);
        DateOnly endDate2 = new DateOnly(DateTime.Now.Year, 03, 10);

        HolidayPeriod holidayPeriodExpected = new HolidayPeriod(startDate, endDate);
        HolidayPeriod holidayPeriodExpected2 = new HolidayPeriod(startDate2, endDate2);


        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected);// to isolate the result
        hpFactoryDouble2.Setup(hpF => hpF.NewHolidayPeriod(startDate2, endDate2)).Returns(holidayPeriodExpected2);// to isolate the result

        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate);
        HolidayPeriod hp2 = _holiday.addHolidayPeriod(hpFactoryDouble2.Object, startDate2, endDate2);

        // act
        int numberOfDaysResult = _holiday.getNumberOfHolidayPeriodsDays();

        // assert
        Assert.Equivalent(expectedValue, numberOfDaysResult);
    }

    // public bool hasHolidayPeriodsDuring(IColaborator colaborator, DateOnly startDate, DateOnly endDate)

    [Fact]
    public void WhenPassingHolidayPeriod_ShouldReturnTrue()
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        var hpFactoryDouble = new Mock<IHolidayPeriodFactory>();

        bool expectedResult = true;

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 10);

        HolidayPeriod holidayPeriodExpected = new HolidayPeriod(startDate, endDate);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected);// to isolate the result

        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate);

        // act
        bool result = _holiday.hasColaboratorAndHolidayPeriodsDuring(_colabDouble.Object, startDate, endDate);

        // assert
        Assert.Equivalent(expectedResult, result);
    }

    [Fact]
    public void WhenPassingOutRangeHolidayPeriod_ShouldReturnFalse()
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        var hpFactoryDouble = new Mock<IHolidayPeriodFactory>();

        bool expectedResult = false;

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 10);
        DateOnly startDateOutRange = new DateOnly(DateTime.Now.Year, 03, 01);
        DateOnly endDateOutRange = new DateOnly(DateTime.Now.Year, 03, 10);

        HolidayPeriod holidayPeriodExpected = new HolidayPeriod(startDate, endDate);

        hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected);// to isolate the result

        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate);

        // act
        bool result = _holiday.hasColaboratorAndHolidayPeriodsDuring(_colabDouble.Object, startDateOutRange, endDateOutRange);

        // assert
        Assert.Equivalent(expectedResult, result);
    }

}
