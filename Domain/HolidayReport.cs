namespace Domain;

public class HolidayReport
{
    private DateOnly _dateReport;
    private bool _holidayResponse;
    private string _strDescription;
    private IColaborator _colaborator;


    public HolidayReport(DateOnly dateReport, bool holidayResponse, string strDescription, IColaborator colaborator)
    {
        if (dateReport > new DateOnly(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day))
        {
            throw new ArgumentException("Invalid Date.");
        }
        
        if (string.IsNullOrWhiteSpace(strDescription))
        {
            throw new ArgumentException("Description can't be empty or null.");
        }

        if (colaborator is null)
        {
            throw new ArgumentException("Colaborator can't be null.");
        }

        this._dateReport = dateReport;
        this._holidayResponse = holidayResponse;
        this._strDescription = strDescription;
        this._colaborator = colaborator;
}
}