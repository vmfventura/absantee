using System.Net;

namespace Domain;

public class Skills
{
    private string _strSkill;
    private int _intSkillLevel;

    public Skills(string strSkill, int intSkillLevel)
    {
        if( !isValidParameters(strSkill)) {
            throw new ArgumentException("Invalid name.");
		}
        if ( !isValidSkillLevel(intSkillLevel))
        {
            throw new ArgumentException("Invalid skill level.");
        }
    }

    private bool isValidParameters(string strSkill)
    {
        if( strSkill is null ||
			strSkill.Length > 50 ||
			string.IsNullOrWhiteSpace(strSkill) ||
			ContainsAny(strSkill, ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]))
        {
			return false;
        }
        return true;
    }

    private bool ContainsAny(string stringToCheck, params string[] parameters)
	{
		return parameters.Any(parameter => stringToCheck.Contains(parameter));
	}

    private bool isValidSkillLevel(int intSkillLevel)
    {
        if (intSkillLevel >= 0 && intSkillLevel <= 5)
        {
            return true;
        }
        return false;
    }
}