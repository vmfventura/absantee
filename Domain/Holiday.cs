namespace Domain;

public class Holiday : IHoliday
{
	public long Id { get; set; }
	public IColaborator _colaborator ;

	public List<HolidayPeriod> _holidayPeriods ;

	protected Holiday() { }

	public Holiday(IColaborator colab)
	{
		if (colab != null)
		{
			_colaborator = colab;
		}
		else
			throw new ArgumentException("Invalid argument: colaborator must be non null");
	}

	public HolidayPeriod addHolidayPeriod(IHolidayPeriodFactory hpFactory, DateOnly startDate, DateOnly endDate)
	{
		HolidayPeriod holidayPeriod = hpFactory.NewHolidayPeriod(startDate, endDate);
		_holidayPeriods.Add(holidayPeriod);
		return holidayPeriod;
	}

	public string getName()
	{
		return _colaborator.getName();
	}

	public List<HolidayPeriod> getHolidayPeriodsDuring(DateOnly startDate, DateOnly endDate)
	{
		return _holidayPeriods.Where(hp => hp.EndDate > startDate && hp.StartDate < endDate)
								.ToList();
	}

	public bool hasColaboratorAndHolidayPeriodsDuring(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
	{
			return _colaborator == colaborator && getHolidayPeriodsDuring(startDate, endDate).Any();
	}

	

	public bool getHolidaysDaysWithMoreThanXDaysOff(int intDaysOff)
	{
		int numberOfDays = 0;

		foreach (HolidayPeriod hp in _holidayPeriods)
		{
			numberOfDays += hp.getNumberOfDays();
		}
		if (numberOfDays >= intDaysOff)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public int getNumberOfHolidayPeriodsDays()
	{
		return _holidayPeriods.Sum(hp => hp.getNumberOfDays());
	}

	public bool hasColaborador(IColaborator colab)
	{
		return this._colaborator == colab ? true : false;
	}
}