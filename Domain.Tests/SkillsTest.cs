
namespace Domain.Tests;

public class SkillsTest
{
    [Theory]
    [InlineData("Teste", 1)]
    [InlineData("Teste", 2)]
    [InlineData("Teste", 3)]
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
        Assert.Throws<ArgumentException>(() => new Skills(strSkill, intSkillLevel));
    }
}