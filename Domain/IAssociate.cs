namespace Domain;

public interface IAssociate
{
    public bool hasColaborador(IColaborator colab);
    public bool isStartDateIsValid(DateOnly startDate, DateOnly endDate);
}