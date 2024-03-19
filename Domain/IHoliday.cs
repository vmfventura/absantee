namespace Domain;

public interface IHoliday
{
    public HolidayPeriod addHolidayPeriod(HolidayPeriodFactory hpFactory, DateOnly startDate, DateOnly endDate);

    public List<HolidayPeriod> getHolidayPeriodsDuring(DateOnly startDate, DateOnly endDate);

}