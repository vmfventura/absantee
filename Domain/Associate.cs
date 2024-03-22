using Domain;

public class Associate : IAssociate
{
    private IColaborator _colaborator;

    private DateOnly _startDate;
    private DateOnly? _endDate;

    public Associate(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
    {
        if(colaborator is null || !isStartDateIsValid(startDate, endDate))
        {
            throw new ArgumentException("Invalid arguments.");
        }

        this._colaborator = colaborator;
        this._startDate = startDate;
        this._endDate = endDate;
    }

    public bool hasColaborador(IColaborator colab)
	{
		return this._colaborator == colab ? true : false;
	}

    public bool isStartDateIsValid(DateOnly startDate, DateOnly endDate)
	{
		if( startDate >= endDate ) 
		{
			return false;
		}
		return true;
	}

    public bool isDateInRange(DateOnly startDate, DateOnly endDate)
    {
        return this._startDate >= startDate && this._endDate <= endDate;
    }

    public bool isColaboratorValidInDateRange(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
    {
        return this.hasColaborador(colaborator) && this.isDateInRange(startDate, endDate);
    }
}