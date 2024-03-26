namespace Domain;

public interface IAssociate
{
    public bool hasColaborador(IColaborator colab);
    public bool isStartDateIsValid(DateOnly startDate, DateOnly? endDate);
    public bool isDateInRange(DateOnly startDate, DateOnly? endDate);
    public bool isColaboratorValidInDateRange(IColaborator colaborator, DateOnly startDate, DateOnly? endDate);
}