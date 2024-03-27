using System.Net;

namespace Domain;

public class Project : IProject
{
    public long Id { get; set; }
    public string _strName { get; set; }

    public DateOnly _dateStart { get; set; }

    public DateOnly? _dateEnd { get; set; }

    public List<IAssociate> _associations = new List<IAssociate>();

    protected Project() {}

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

    public bool isValidParameters(string strName, DateOnly dateStart, DateOnly? dateEnd)
    { 
        if( strName==null || strName.Length > 50 || string.IsNullOrWhiteSpace(strName) ||
            (dateStart > dateEnd) )
        {
			return false;
        }
        return true;
    }

    public List<IAssociate> getListByColaborator(IColaborator colaborator)
    {        
        return _associations.Where(a => a.hasColaborador(colaborator)).ToList();
    }


    public List<IAssociate> getListByColaboratorInRange(IColaborator colaborator, DateOnly startDate, DateOnly? endDate)
    {        
        return _associations.Where(a => a.isColaboratorValidInDateRange(colaborator, startDate, endDate)).ToList();
    }

    public List<IColaborator> getListColaboratorByProject()
    {
        return _associations.Select(a => a.getColaborator()).Distinct().ToList();
    }

}