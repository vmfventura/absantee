namespace Domain;

public interface IHoliday
{
    public HolidayPeriod addHolidayPeriod(IHolidayPeriodFactory hpFactory, DateOnly startDate, DateOnly endDate);
    public string getName();

    public List<HolidayPeriod> getHolidayPeriodsDuring(DateOnly startDate, DateOnly endDate);

    public bool hasColaboratorAndHolidayPeriodsDuring(IColaborator colaborator, DateOnly startDate, DateOnly endDate);

    public int getHolidaysDaysWithMoreThanXDaysOff(int intDaysOff);

    public int getNumberOfHolidayPeriodsDays();

    public bool hasColaborador(IColaborator colab);
}