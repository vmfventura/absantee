namespace Domain;

public class Holiday : IHoliday
{
	private IColaborator _colaborator;

	private List<HolidayPeriod> _holidayPeriods = new List<HolidayPeriod>();

	public IColaborator Colaborador
	{
		get { return _colaborator; }
	}
	
	public List<HolidayPeriod> HolidayPeriods
	{
		get { return _holidayPeriods; }
	}

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
								.Select(hp => new HolidayPeriod(hp.StartDate < startDate ? startDate : hp.StartDate,
											hp.EndDate > endDate ? endDate : hp.EndDate))
								.ToList();
	}

	public bool hasHolidayPeriodsDuring(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
	{
			return _colaborator == colaborator && getHolidayPeriodsDuring(startDate, endDate).Any();
	}

	public int getHolidaysDaysWithMoreThanXDaysOff(int intDaysOff)
	{
		int numberOfDays = 0;

		foreach (HolidayPeriod hp in _holidayPeriods)
		{
			numberOfDays += hp.getNumberOfDays();
		}
		if (numberOfDays > intDaysOff)
		{
			return numberOfDays;
		}
		else
		{
			return 0;
		}
	}	

	public List<HolidayPeriod> getListHoliday()
	{
		return _holidayPeriods;
	}

	public int getNumberOfDaysByColaborator()
	{
		return _holidayPeriods.Sum(hp => hp.getNumberOfDays());
	}

}