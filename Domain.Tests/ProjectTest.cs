namespace Domain.Tests;
public class ProjectTest
{
    public static readonly object[][] SuccessCasesWithAssociations =
    [
        ["Teste", new DateOnly(DateTime.Now.Year, 01, 01), new DateOnly(DateTime.Now.Year, 12, 01)],
        ["Teste", new DateOnly(DateTime.Now.Year, 10, 01), null],
        ["Teste", new DateOnly(DateTime.Now.Year, 10, 01), new DateOnly(DateTime.Now.Year, 10, 01)],
        ["TesteTesteTesteTesteTeTesteTesteTestesteTesteTeste", new DateOnly(DateTime.Now.AddYears(1).Year, 01, 01), null]
    ];
    [Theory]
    [MemberData(nameof(SuccessCasesWithAssociations))]
    public void WhenPassingCorrectListData_ThenProjectIsInstantiated(string strName, DateOnly dateStart, DateOnly? dateEnd)
    {
        var project = new Project(strName, dateStart, dateEnd);
    }
    // name can't be null, empty or white space
    public static readonly object[][] FailedNameCases =
    [
        ["", new DateOnly(DateTime.Now.Year,01,01), new DateOnly(DateTime.Now.Year,12,01)],
        ["                ", new DateOnly(DateTime.Now.Year,10,01), new DateOnly(DateTime.Now.Year,12,31)],
        [null, new DateOnly(DateTime.Now.Year,10,01), new DateOnly(DateTime.Now.Year,12,31)],
        ["Teste", new DateOnly(DateTime.Now.Year,12,01), new DateOnly(DateTime.Now.Year,10,31)],
        ["Teste", new DateOnly(DateTime.Now.Year,12,02), new DateOnly(DateTime.Now.Year,12,01)]
    ];
    [Theory, MemberData(nameof(FailedNameCases))]
    public void WhenPassingInvalidName_ThenThrowsException( string strName, DateOnly dataStart, DateOnly dataEnd)
    {
        // act
        var ex = Assert.Throws<ArgumentException>(() => new Project(strName, dataStart, dataEnd));
        // assert
        Assert.Equal("Invalid arguments.", ex.Message);
    }
    [Fact]
    public void WhenPassingColaborator_ShouldReceiveListOfAssociates()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startProjectDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 12, 01);
        Project project = new Project("Project", startProjectDate, null);
        Mock<IAssociateFactory> associateFactoryDouble = new Mock<IAssociateFactory>();
        Mock<Associate> associateDouble = new Mock<Associate>(colabDouble.Object, startDate, null);
        associateFactoryDouble
            .Setup(x => x.NewAssociate(colabDouble.Object, startDate, null))
            .Returns(associateDouble.Object);
        Associate associate = project.addAssociate(associateFactoryDouble.Object, colabDouble.Object, startDate, null);
        // act
        var result = project.getListByColaborator(colabDouble.Object);
        // assert
        Assert.Equal(1, result.Count);
    }
    [Fact]
    public void WhenPassingWrongColaborator_ShouldReceiveListOfAssociates()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Mock<IColaborator> colabDouble2 = new Mock<IColaborator>();
        DateOnly startProjectDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 12, 01);
        Project project = new Project("Project", startProjectDate, null);
        Mock<IAssociateFactory> associateFactoryDouble = new Mock<IAssociateFactory>();
        Mock<Associate> associateDouble = new Mock<Associate>(colabDouble.Object, startDate, null);
        associateFactoryDouble
            .Setup(x => x.NewAssociate(colabDouble.Object, startDate, null))
            .Returns(associateDouble.Object);
        Associate associate = project.addAssociate(associateFactoryDouble.Object, colabDouble.Object, startDate, null);
        // act
        var result = project.getListByColaborator(colabDouble2.Object);
        // assert
        Assert.Empty(result);
    }
    [Fact]
    public void WhenPassingColaboratorAndDateInRange_ShouldReceiveListOfAssociates()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startProjectDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endProjectDate = new DateOnly(DateTime.Now.Year, 12, 31);
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 12, 01);
        DateOnly startRangeDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endRangeDate = new DateOnly(DateTime.Now.Year, 12, 01);
        Project project = new Project("Project", startProjectDate, endProjectDate);
        Mock<IAssociateFactory> associateFactoryDouble = new Mock<IAssociateFactory>();
        Mock<Associate> associateDouble = new Mock<Associate>(colabDouble.Object, startDate, endDate);
        associateFactoryDouble
            .Setup(x => x.NewAssociate(colabDouble.Object, startDate, endDate))
            .Returns(associateDouble.Object);
        Associate associate = project.addAssociate(associateFactoryDouble.Object, colabDouble.Object, startDate, endDate);
        // act
        var result = project.getListByColaboratorInRange(colabDouble.Object, startRangeDate, endRangeDate);
        // assert
        Assert.Equal(1, result.Count);
    }
    [Fact]
    public void WhenPassingColaboratorAndWrongDateInRange_ShouldReceiveEmptyListOfAssociates()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startProjectDate = new DateOnly(DateTime.Now.Year, 01, 01);
        DateOnly endProjectDate = new DateOnly(DateTime.Now.Year, 12, 31);
        DateOnly startDate = new DateOnly(DateTime.Now.AddYears(-1).Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.AddYears(-1).Year, 12, 01);
        DateOnly startRangeDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endRangeDate = new DateOnly(DateTime.Now.Year, 12, 01);
        Project project = new Project("Project", startProjectDate, endProjectDate);
        Mock<IAssociateFactory> associateFactoryDouble = new Mock<IAssociateFactory>();
        Mock<Associate> associateDouble = new Mock<Associate>(colabDouble.Object, startDate, endDate);
        associateFactoryDouble
            .Setup(x => x.NewAssociate(colabDouble.Object, startDate, endDate))
            .Returns(associateDouble.Object);
        Associate associate = project.addAssociate(associateFactoryDouble.Object, colabDouble.Object, startDate, endDate);
        // act
        var result = project.getListByColaboratorInRange(colabDouble.Object, startRangeDate, endRangeDate);
        // assert
        Assert.Empty(result);
    }
}