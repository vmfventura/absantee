using Domain;

public class Associate : IAssociate
{
    private IColaborator _colaborator;

    public Associate(IColaborator colaborator)
    {
        if(colaborator is null)
        {
            throw new ArgumentException("Colaborator can't be null");
        }

        this._colaborator = colaborator;        
    }

    public IColaborator getColaborator()
	{
		return _colaborator;
	}
}