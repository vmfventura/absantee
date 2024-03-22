using System.Net;

namespace Domain;

public class Project : IProject
{
    private string _strName;

    private DateOnly _dateStart;

    private DateOnly? _dateEnd;

    private List<Associate> _associations = new List<Associate>();

    public DateOnly StartDate
	{
		get { return _dateStart; }
	}

	public DateOnly? EndDate
	{
		get { return _dateEnd; }
	}

    public Project(string strName, DateOnly dateStart, DateOnly? dateEnd, List<Associate> associations)
    {
        if( !isValidParameters(strName, dateStart, dateEnd, associations) ) {
            throw new ArgumentException("Invalid name.");
		}

        this._strName = strName;
        this._dateStart = dateStart;
        this._dateEnd = dateEnd;
        this._associations = associations;
    }

    private bool isValidParameters(string strName, DateOnly dateStart, DateOnly? dateEnd, List<Associate> associations)
    { 
        if( strName==null || strName.Length > 50 || string.IsNullOrWhiteSpace(strName) ||
            (dateStart > dateEnd) || (associations is null))
        {
			return false;
        }
        return true;
    }

    public List<Associate> getListByColaborator(IColaborator colaborator)
    {        
        return _associations.Where(a => a.hasColaborador(colaborator)).ToList();
    }

}