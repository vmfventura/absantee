namespace Domain;

public interface IHoliday
{
    public HolidayPeriod addHolidayPeriod(IHolidayPeriodFactory hpFactory, DateOnly startDate, DateOnly endDate);

    public List<HolidayPeriod> getHolidayPeriodsDuring(DateOnly startDate, DateOnly endDate);

}