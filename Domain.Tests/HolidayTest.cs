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
        Assert.Throws<ArgumentException>(() => new Holiday(null));
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

    public static readonly object[][] SucessCases =
    [
        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,12,01)], // dateStart < dateEnd
        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,01,02)] // dateStart < dateEnd ( 1 day)
    ];
    [Theory, MemberData(nameof(SucessCases))]
    public void WhenRequestingaddHolidayPeriod_ThenReturnHolidayPeriod(DateOnly startDate, DateOnly endDate)
    {

        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Holiday holidayDouble = new Holiday(colabDouble.Object);
        Mock<HolidayPeriodFactory> hpFactoryDouble = new Mock<HolidayPeriodFactory>();
        HolidayPeriod holidayPeriodExpected = new HolidayPeriod(startDate, endDate);

        // holidayDouble.Setup(h => h.addHolidayPeriod(startDate, endDate)).Returns(holidayPeriodExpected); // to isolate the result

        // act
        HolidayPeriod holidayPeriod = holidayDouble.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate); // to get the actual result

        // assert
        Assert.Equivalent(holidayPeriodExpected, holidayPeriod); // compare objects
    }

    public static readonly object[][] SucessHolidayDatesCases =
    [
        [new DateOnly(DateTime.Now.Year,02,01), new DateOnly(DateTime.Now.Year,03,30),
        new List<HolidayPeriod>
        {
            new HolidayPeriod(
                new DateOnly(DateTime.Now.Year, 02, 01),
                new DateOnly(DateTime.Now.Year, 03, 20))
        }], // dateStart < dateEnd

        [new DateOnly(DateTime.Now.Year,07,01), new DateOnly(DateTime.Now.Year,08,02),
        new List<HolidayPeriod>
        {
            new HolidayPeriod(
                new DateOnly(DateTime.Now.Year, 07, 31),
                new DateOnly(DateTime.Now.Year, 08, 02))
        }], // dateStart < dateEnd ( 1 day)

        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,12,02),
        new List<HolidayPeriod>
        {
            new HolidayPeriod(
                new DateOnly(DateTime.Now.Year, 02, 01),
                new DateOnly(DateTime.Now.Year, 03, 20)),
            new HolidayPeriod(
                new DateOnly(DateTime.Now.Year, 07, 31),
                new DateOnly(DateTime.Now.Year, 08, 15))
        }] // dateStart < dateEnd ( 1 day)
    ];
    [Theory, MemberData(nameof(SucessHolidayDatesCases))]
    public void TestGetHolidayPeriodsDuring_CompletelyWithinRange(DateOnly startDate, DateOnly endDate, List<HolidayPeriod> holidayPeriodExpected)
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        Mock<HolidayPeriodFactory> hpFactoryDouble = new Mock<HolidayPeriodFactory>();


        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, new DateOnly(DateTime.Now.Year, 02, 01), new DateOnly(DateTime.Now.Year, 03, 20));
        HolidayPeriod hp2 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, new DateOnly(DateTime.Now.Year, 07, 31), new DateOnly(DateTime.Now.Year, 08, 15));

        // act
        List<HolidayPeriod> _holidayPeriods = _holiday.getHolidayPeriodsDuring(startDate, endDate);
        // assert
        Assert.Equivalent(holidayPeriodExpected, _holidayPeriods);
    }

    public static readonly object[][] FailedHolidayDatesCases =
    [
        [new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,01,30),
        new List<HolidayPeriod>
        {
        }], // List of Holiday Period is out of range
        [new DateOnly(DateTime.Now.Year,09,01), new DateOnly(DateTime.Now.Year,10,02),
        new List<HolidayPeriod>
        {
        }] // List of Holiday Period is out of range
    ];
    [Theory, MemberData(nameof(FailedHolidayDatesCases))]
    public void TestGetHolidayPeriodsDuring_OutOfRange(DateOnly startDate, DateOnly endDate, List<HolidayPeriod> holidayPeriodExpected)
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        Mock<HolidayPeriodFactory> hpFactoryDouble = new Mock<HolidayPeriodFactory>();

        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, new DateOnly(DateTime.Now.Year, 02, 01), new DateOnly(DateTime.Now.Year, 03, 20));
        HolidayPeriod hp2 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, new DateOnly(DateTime.Now.Year, 07, 31), new DateOnly(DateTime.Now.Year, 08, 15));

        // act
        List<HolidayPeriod> _holidayPeriods = _holiday.getHolidayPeriodsDuring(startDate, endDate);

        // assert
        Assert.Equivalent(holidayPeriodExpected, _holidayPeriods);
    }


    [Fact]
    public void WhenPassingLowerNumberOfDays_GetCorrectNumberOfDays()
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        Mock<HolidayPeriodFactory> hpFactoryDouble = new Mock<HolidayPeriodFactory>();
        int expectedValue = 9;
        int numberOfDays = 5;

        DateOnly startDate1 = new DateOnly(DateTime.Now.Year, 02, 01);

        DateOnly endDate1 = new DateOnly(DateTime.Now.Year, 02, 10);

        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate1, endDate1);

        // act
        int numberOfDaysResult = _holiday.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays);
        // assert
        Assert.Equivalent(expectedValue, numberOfDaysResult);
    }


    [Fact]
    public void WhenPassingHigherNumberOfDays_GetZeroNumberOfDays()
    {
        // arrange
        Mock<IColaborator> _colabDouble = new Mock<IColaborator>();
        Holiday _holiday = new Holiday(_colabDouble.Object);
        var hpFactoryDouble = new Mock<HolidayPeriodFactory>();

        int expectedValue = 0;
        int numberOfDays = 5;

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 04);

        HolidayPeriod hp1 = _holiday.addHolidayPeriod(hpFactoryDouble.Object, startDate, endDate);

        // act
        int numberOfDaysResult = _holiday.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays);

        // assert
        Assert.Equivalent(expectedValue, numberOfDaysResult);
    }

    // [Fact]
    // public void WhenRequestingAddHolidayPeriod_ThenReturnHolidayPeriod()
    // {
    //     // arrange
    //     Mock<IColaborator> doubleColaborator = new Mock<IColaborator>();
    //     var holiday = new Holiday(doubleColaborator.Object);

    //     DateOnly dataInicio = new DateOnly(2024, 7, 1);
    //     DateOnly dataFim = new DateOnly(2024, 7, 15);

    //     Mock<IHolidayPeriodFactory> hpFactoryDouble = new Mock<IHolidayPeriodFactory>();

    //     Mock<HolidayPeriod> hpDouble = new Mock<HolidayPeriod>(dataInicio, dataFim);

    //     hpFactoryDouble.Setup(hpF => hpF.NewHolidayPeriod(dataInicio, dataFim)).Returns(hpDouble.Object);

    //     // act
    //     DateOnly hpDataInicio = new DateOnly(2024, 7, 1);
    //     DateOnly hpDataFim = new DateOnly(2024, 7, 15);

    //     var holidayPeriod = holiday.addHolidayPeriod(hpFactoryDouble.Object, hpDataInicio, hpDataFim);


    //     // assert
    //     Assert.Equal(dataInicio, holidayPeriod.getStartDate());
    //     Assert.Equal(dataFim, holidayPeriod.getEndDate());
    // }

}
