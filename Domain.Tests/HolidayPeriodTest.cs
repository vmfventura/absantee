namespace Domain.Tests;

public class HolidayPeriodTest
{    
    public static readonly object[][] SucessCases =
    [
        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,12,01)], // dateStart < dateEnd
        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,01,02)] // dateStart < dateEnd ( 1 day)
    ];
    [Theory, MemberData(nameof(SucessCases))]
    public void WhenPassingCorrectData_ThenHolidayPeriodIsInstantiated(DateOnly dateStart, DateOnly dateEnd)
    {
        new HolidayPeriod(dateStart, dateEnd);
    }

    
    public static readonly object[][] FailedCases =
    [
        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,01,01)], // dateStart = dateEnd
        [new DateOnly(DateTime.Now.Year,02,01), new DateOnly(DateTime.Now.Year,01,02)] // dateStart > dateEnd
    ];
    [Theory, MemberData(nameof(FailedCases))]
    public void WhenPassingInvalidData_ThenThrowsException(DateOnly dateStart, DateOnly dateEnd)
    {
        var ex = Assert.Throws<ArgumentException>(() => new HolidayPeriod(dateStart, dateEnd));

        Assert.Equal("Invalid arguments: start date >= end date.", ex.Message);
    }

    public static readonly object[][] SucessDatesCases =
    [
        [30, new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,01,31)], // dateStart < dateEnd
        [1, new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,01,02)] // dateStart < dateEnd ( 1 day)

    ];
    [Theory, MemberData(nameof(SucessDatesCases))]
    public void WhenPassingValidDates_GetCorrectResult(int expectedValue, DateOnly startDate, DateOnly endDate)
    {
        // arrange
        Mock<HolidayPeriod> _holidayPeriod = new Mock<HolidayPeriod>(startDate, endDate);
        
        // act
        int result = _holidayPeriod.Object.getNumberOfDays();

        // assert
        Assert.Equal(expectedValue, result);
    }

}