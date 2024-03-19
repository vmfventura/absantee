namespace Domain.Tests;

public class ProjectTest
{
    public static readonly object[][] SuccessCasesWithAssociations =
    [
        ["Teste", new DateOnly(DateTime.Now.Year, 01, 01), new DateOnly(DateTime.Now.Year, 12, 01), new List<Associate>()],
        ["Teste", new DateOnly(DateTime.Now.Year, 10, 01), null, new List<Associate>()],
        ["Teste", new DateOnly(DateTime.Now.AddYears(1).Year, 01, 01), null, new List<Associate>()]
    ];

    [Theory]
    [MemberData(nameof(SuccessCasesWithAssociations))]
    public void WhenPassingCorrectListData_ThenProjectIsInstantiated(string strName, DateOnly dateStart, DateOnly? dateEnd, List<Associate> associations)
    {
        var project = new Project(strName, dateStart, dateEnd, associations);
    }

    // name can't be null, empty or white space
    public static readonly object[][] FailedNameCases =
    [
        ["", new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,12,01), new List<Associate>()],
        ["                ", new DateOnly(DateTime.Now.Year,10,01), new DateOnly(DateTime.Now.Year,12,31), new List<Associate>()],
        [null, new DateOnly(DateTime.Now.Year,10,01), new DateOnly(DateTime.Now.Year,12,31), new List<Associate>()]
    ];

    [Theory, MemberData(nameof(FailedNameCases))]
    public void WhenPassingInvalidName_ThenThrowsException( string strName, DateOnly dataStart, DateOnly dataEnd, List<Associate> associations)
    {
        Assert.Throws<ArgumentException>(() => new Project(strName, dataStart, dataEnd, associations));
    }

    // dateStart > dateEnd
    // dateStart in the future (+ 1 year) and dateEnd in the present (year)
    // public static readonly object[][] FailedDates =
    // [
    //     ["Teste", new DateOnly(DateTime.Now.AddYears(1).Year,01,01), new DateOnly(DateTime.Now.Year,12,01)],
    //     ["Novo Test", new DateOnly(DateTime.Now.Year,12,01), new DateOnly(DateTime.Now.Year,01,01)],
    //     ["Apenas um nome mais comprido", new DateOnly(DateTime.Now.AddYears(1).Year,10,01), new DateOnly(DateTime.Now.Year,10,01)]
    // ];

    // [Theory, MemberData(nameof(FailedDates))]
    // public void WhenPassingInvalidDates_ThenThrowsException( string strName, DateOnly dataStart, DateOnly dataEnd)
    // {
    //     // Mock<IColaborator> colabDouble = new Mock<IColaborator>();
    //     Mock<List<IAssociate>> listMock = new Mock<List<IAssociate>>();
        

    //     Assert.Throws<ArgumentException>(() => new Project(strName, dataStart, dataEnd, listMock.Object));
    // }
}