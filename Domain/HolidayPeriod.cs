using System.Net;

namespace Domain;

public class HolidayPeriod
{
	public long Id { get; set; }
	// public DateOnly _startDate { get; set; }
	// public DateOnly _endDate { get; set; }

	private DateOnly _startDate;
	public DateOnly StartDate
	{
		get { return _startDate; }
		set { _startDate = value; }
	}
	private DateOnly _endDate;
	public DateOnly EndDate
	{
		get { return _endDate; }
		set { _endDate = value; }
	}
	

	int _status;

	protected HolidayPeriod() { }

	// public DateOnly StartDate
	// {
	// 	get { return _startDate; }
	// }

	// public DateOnly EndDate
	// {
	// 	get { return _endDate; }
	// }

	public HolidayPeriod(DateOnly startDate, DateOnly endDate)
	{
		if (!isStartDateIsValid(startDate, endDate))
		{
			throw new ArgumentException("Invalid arguments: start date >= end date.");
		}
		
		this._startDate = startDate;
		this._endDate = endDate;
	}

	public bool isStartDateIsValid(DateOnly startDate, DateOnly endDate)
	{
		if( startDate >= endDate ) 
		{
			return false;
		}
		return true;
	}

	public int getNumberOfDays()
	{
		int startDateDays = _startDate.DayNumber;
		int endDateDays = _endDate.DayNumber;
		return endDateDays - startDateDays;
	}
}

