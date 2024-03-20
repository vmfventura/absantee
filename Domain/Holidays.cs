using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Holidays : IHolidays
    {
        private List<Holiday> _holidayList = new List<Holiday>();

        public Holidays(List<Holiday> holidayList)
        {
            if (holidayList is not null)
            {
                _holidayList = holidayList;
            }
            else
            {
                throw new ArgumentException("Holidays cannot be null");
            }
        }

        public Holiday addHoliday(IHolidayFactory hFactory, IColaborator colaborator)
        {
            Holiday holiday = hFactory.NewHoliday(colaborator);
            _holidayList.Add(holiday);
            return holiday;
        }

        public List<Holiday> GetListHolidayMoreDays(int numberOfDays)
        {
            return _holidayList.Where(h => h.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays) > 0).ToList();
        }

        public List<Holiday> getListHolidayFilterByColaborator(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
        {
            IEnumerable<Holiday> holidayList = _holidayList.Where(h => h.hasHolidayPeriodsDuring(colaborator, startDate, endDate)); 

            if (!holidayList.Any())
            {
                throw new ArgumentException("No holiday found for this colaborator");
            }

            return holidayList.ToList();
        }

        public int getNumberOfHolidaysDaysForColaboratorDuringPeriod(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
        {
            int totalDaysOff = _holidayList
                .Where(h => h.hasHolidayPeriodsDuring(colaborator, startDate, endDate))
                .Sum(holiday => holiday.getNumberOfDaysByColaborator());

            return totalDaysOff;
        }

        public int getNumberOfDaysByColaborator(IColaborator colaborator)
        {
            return _holidayList.Where(h => h.hasColaborador(colaborator))
                                .Sum(h => h.getNumberOfDaysByColaborator());
        }
    }
}