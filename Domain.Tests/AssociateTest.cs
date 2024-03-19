namespace Domain.Tests;

public class AssociateTest
{
    [Fact]
    public void WhenPassingCorrectColaborator_ThenAssociateIsInstantiated()
    {
        Mock<IColaborator> colabDouble = new Mock<IColaborator>();

        new Associate(colabDouble.Object);
    }

    [Fact]
    public void WhenPassingNullAsColaborator_ThenThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Associate(null));
    }
}