using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Holidays
    {
        private HolidayFactory _holidayFactory;
        private List<Holiday> _holidayList = new List<Holiday>();

        public Holidays(HolidayFactory hFactory)
        {
            if (hFactory is not null)
            {
                _holidayFactory = hFactory;
            }
            else
            {
                throw new ArgumentException("Holiday Factory cannot be null");
            }
        }

        public Holiday addHolidaysList(IColaborator colaborator)
        {
            Holiday holiday = _holidayFactory.NewHoliday(colaborator);
            _holidayList.Add(holiday);
            return holiday;
        }

        public List<Holiday> GetListHolidayMoreDays(int numberOfDays)
        {
            return _holidayList.Where(h => h.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays) > 0).ToList();
        }

        public List<Holiday> getListHolidayFilterByColaborator(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
        {
            IEnumerable<Holiday> holidayList = _holidayList.Where(h => h.hasColaboratorAndHolidayPeriodsDuring(colaborator, startDate, endDate)); 

            if (!holidayList.Any())
            {
                throw new ArgumentException("No holiday found for this colaborator");
            }

            return holidayList.ToList();
        }

        public int getNumberOfHolidaysDaysForColaboratorDuringPeriod(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
        {
            int totalDaysOff = _holidayList
                .Where(h => h.hasColaboratorAndHolidayPeriodsDuring(colaborator, startDate, endDate))
                .Sum(holiday => holiday.getNumberOfHolidayPeriodsDays());

            return totalDaysOff;
        }

        public int getNumberOfDaysByColaborator(IColaborator colaborator)
        {
            return _holidayList.Where(h => h.hasColaborador(colaborator))
                                .Sum(h => h.getNumberOfHolidayPeriodsDays());
        }
    }
}