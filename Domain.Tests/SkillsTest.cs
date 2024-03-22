
namespace Domain.Tests;

public class SkillsTest
{
    [Theory]
    [InlineData("Teste", 0)]
    [InlineData("Teste", 1)]
    [InlineData("Teste", 2)]
    [InlineData("Teste", 3)]
    [InlineData("Teste", 4)]
    [InlineData("Teste", 5)]
    [InlineData("kasdjflkadjf lkasdfj laksdjf alkdsfjv alkdsfjv asl", 5)]
    public void WhenPassingCorrectData_ThenSkillsIsInstantiated(string strSkill, int intSkillLevel)
    {
        new Skills(strSkill, intSkillLevel);
    }

    [Theory]
    [InlineData("Teste", -1)]
    [InlineData("Teste", 6)]
    [InlineData("Teste", 100)]
    public void WWhenPassingInvalidSkills_ThenThrowsException(string strSkill, int intSkillLevel)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Skills(strSkill, intSkillLevel));

        // assert

        Assert.Equal("Invalid skill level.", ex.Message);
    }

    [Theory]
    [InlineData("", 1)]
    [InlineData("abasdfsc 12", 2)]
    [InlineData("     ", 3)]
    [InlineData("kasdjflkadjf lkasdfj laksdjf alkdsfjv alkdsfjv asldkfj asldkfvj asdlkvj asdlkfvj asdlkfvj asdflkfvj asfldkjfv jasdflkvj lasf", 4)]
    [InlineData(null, 5)]
    public void WhenPassingInvalidName_ThenThrowsException(string strSkill, int intSkillLevel)
    {
        var ex = Assert.Throws<ArgumentException>(() => new Skills(strSkill, intSkillLevel));

        // assert

        Assert.Equal("Invalid name.", ex.Message);
    }
}