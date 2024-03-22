namespace Domain.Tests;

public class AssociateTest
{
    [Fact]
    public void WhenPassingCorrectColaborator_ThenAssociateIsInstantiated()
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);

        new Associate(colabDouble.Object, startDate, endDate);
    }

    [Fact]
    public void WhenPassingNullAsColaborator_ThenThrowsException()
    {
        // arrange
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);

        // act
        var ex = Assert.Throws<ArgumentException>(() => new Associate(null, startDate, endDate));
        // assert
        Assert.Equal("Invalid arguments.", ex.Message);
    }

    [Fact]
    public void WhenPassingNullStartDate_ThenThrowsException()
    {
        // arrange
        DateOnly? startDate = null; // Use nullable DateOnly
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);
    
        // act
        var ex = Assert.Throws<ArgumentException>(() => new Associate(null, startDate ?? default, endDate));
        // assert
        Assert.Equal("Invalid arguments.", ex.Message);
    }

    [Fact]
    public void WhenPassingWrongDates_ThenThrowsException()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startDate = new DateOnly(DateTime.Now.AddYears(1).Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);

        // act
        var ex = Assert.Throws<ArgumentException>(() => new Associate(colabDouble.Object, startDate, endDate));
        // assert
        Assert.Equal("Invalid arguments.", ex.Message);
    }

    [Fact]
    public void IfEndDateIsBeforeStartDate_ThenThrowsException()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startDate = new DateOnly(DateTime.Now.AddYears(1).Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);
        // act
        var ex = Assert.Throws<ArgumentException>(() => new Associate(colabDouble.Object, startDate, endDate));
        // assert
        Assert.Equal("Invalid arguments.", ex.Message);
    }
    [Fact]
    public void IfEndDateIsEqualStartDate_ThenThrowsException()
    {
        // arrange
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 01);
        // act
        var ex = Assert.Throws<ArgumentException>(() => new Associate(colabDouble.Object, startDate, endDate));
        // assert
        Assert.Equal("Invalid arguments.", ex.Message);
    }

    [Fact]
    public void CheckIfColaboratorIsValid_ShouldReturnTrue()
    {
        // arrange
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Associate associate = new Associate(colabDouble.Object, startDate, endDate);

        // act
        var result = associate.hasColaborador(colabDouble.Object);

        // assert
        Assert.True(result);
    }

    [Fact]
    public void CheckIfWrongColaboratorIsValid_ShouldReturnFalse()
    {
        // arrange
        DateOnly startDate = new DateOnly(DateTime.Now.Year, 02, 01);
        DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 11);
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();
        Associate associate = new Associate(colabDouble.Object, startDate, endDate);
        Mock<IColaborator> colabDouble2 = new Mock<IColaborator>();
        Associate associate2 = new Associate(colabDouble.Object, startDate, endDate);

        // act
        var result = associate.hasColaborador(colabDouble2.Object);

        // assert
        Assert.False(result);
    }
}