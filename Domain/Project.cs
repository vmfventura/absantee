using System.Net;

namespace Domain;

public class Project : IProject
{
    private string _strName;

    private DateOnly _dateStart;

    private DateOnly? _dateEnd;

    private List<Associate> _associations = new List<Associate>();

    public Project(string strName, DateOnly dateStart, DateOnly? dateEnd)
    {
        if( !isValidParameters(strName, dateStart, dateEnd) ) {
            throw new ArgumentException("Invalid arguments.");
		}

        this._strName = strName;
        this._dateStart = dateStart;
        this._dateEnd = dateEnd;
    }

    public Associate addAssociate(IAssociateFactory aFactory, IColaborator colaborator, DateOnly startDate, DateOnly? endDate)
    {
        Associate associate = aFactory.NewAssociate(colaborator, startDate, endDate);
        _associations.Add(associate);
        return associate; 
    }

    private bool isValidParameters(string strName, DateOnly dateStart, DateOnly? dateEnd)
    { 
        if( strName==null || strName.Length > 50 || string.IsNullOrWhiteSpace(strName) ||
            (dateStart > dateEnd) )
        {
			return false;
        }
        return true;
    }

    public List<Associate> getListByColaborator(IColaborator colaborator)
    {        
        return _associations.Where(a => a.hasColaborador(colaborator)).ToList();
    }


    public List<Associate> getListByColaboratorInRange(IColaborator colaborator, DateOnly startDate, DateOnly? endDate)
    {        
        return _associations.Where(a => a.isColaboratorValidInDateRange(colaborator, startDate, endDate)).ToList();
    }

}