namespace Domain.Tests;

public class HolidayReportTest
{
    [Fact]
    public void WhenPassingWrongDates_ThenThrowsException()
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startDate = new DateOnly(DateTime.Now.AddYears(1).Year, 1, 1);
        string description = "A Valid Description";

        // HolidayReport holidayReport = new HolidayReport(startDate, true, description, colabDouble.Object);

        // act
        var ex = Assert.Throws<ArgumentException>(() => new HolidayReport(startDate, true, description, colabDouble.Object));

        // assert
        Assert.Equal("Invalid Date.", ex.Message);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("          ")]
    public void WhenPassingNullDescription_ThenThrowsException(string description)
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);

        // HolidayReport holidayReport = new HolidayReport(startDate, true, description, colabDouble.Object);

        // act
        var ex = Assert.Throws<ArgumentException>(() => new HolidayReport(startDate, true, description, colabDouble.Object));

        // assert
        Assert.Equal("Description can't be empty or null.", ex.Message);
    }

    [Fact]
    public void WhenPassingNullColaborator_ThenThrowsException()
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
        string description = "A Valid Description";

        // HolidayReport holidayReport = new HolidayReport(startDate, true, description, colabDouble.Object);

        // act
        var ex = Assert.Throws<ArgumentException>(() => new HolidayReport(startDate, true, description, null));

        // assert
        Assert.Equal("Colaborator can't be null.", ex.Message);
    }

    // Correct data 
    public static readonly object[][] SucessCases =
    [
        [new DateOnly(DateTime.Now.Year,01,01),true, "New year vacations"], // first day of the year, with true response
        [new DateOnly(DateTime.Now.Year,01,01),false, "Vacations with the family"], // first day of the year, with false response
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),false, "No, it's not my day"], // current day
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day),false, "No, it's not your day"] // minus one day
    ];
    [Theory, MemberData(nameof(SucessCases))]
    public void WhenPassingCorrectData_ThenHolidayReportIsInstantiated(DateOnly dateReport, bool holidayResponse, string strDescription) 
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        new HolidayReport(dateReport, holidayResponse, strDescription, colabDouble.Object);

    }

    // Invalid dates
    public static readonly object[][] FailedDateCases =
    [
        [new DateOnly(DateTime.Now.Year,DateTime.Now.AddMonths(1).Month,01),true, "New year vacations"], // One more month
        [new DateOnly(DateTime.Now.AddYears(1).Year,01,01),false, "Vacations with the family"], // One more year
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day),false, "No, it's not my day"] // One more day
    ];

    
    [Theory, MemberData(nameof(FailedDateCases))]
    public void WhenPassingIncorrectDate_ThenThrowsExpception(DateOnly dateReport, bool holidayResponse, string strDescription) 
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        Assert.Throws<ArgumentException>(() => new HolidayReport(dateReport, holidayResponse, strDescription, colabDouble.Object));

    }

    // Invalid description
    public static readonly object[][] FailedDescriptionCases =
    [
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day),false, ""], // empty string
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),false, "                      "], // whitespaces
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),false, null] // null
    ];
    [Theory, MemberData(nameof(FailedDescriptionCases))]
    public void WhenPassingIncorrectDescription_ThenThrowsExpception(DateOnly dateReport, bool holidayResponse, string strDescription) 
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        Assert.Throws<ArgumentException>(() => new HolidayReport(dateReport, holidayResponse, strDescription, colabDouble.Object));

    }

    // Null colaborator
    public static readonly object[][] FailedColaboratorCases =
    [
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(-1).Day),false, ""],
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day),false, null],
        [new DateOnly(DateTime.Now.AddYears(1).Year,01,01),false, "Vacations with the family"],
        [new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.AddDays(1).Day),false, "No, it's not my day"]
    ];
    [Theory, MemberData(nameof(FailedColaboratorCases))]
    public void WhenPassingNullAsColaborator_ThenThrowsException(DateOnly dateReport, bool holidayResponse, string strDescription)
    {
        Assert.Throws<ArgumentException>(() => new HolidayReport(dateReport, holidayResponse, strDescription, null));
    }
}