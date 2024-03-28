using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Holidays : IHolidays
    {
        public long Id { get; set; }
        public IHolidayFactory _holidayFactory;
        public List<IHoliday> _holidayList = new List<IHoliday>();
        
        protected Holidays() {}
        public Holidays(IHolidayFactory hFactory)
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

        public IHoliday addHoliday(IColaborator colaborator)
        {
            IHoliday holiday = _holidayFactory.NewHoliday(colaborator);
            _holidayList.Add(holiday);
            return holiday;
        }

        public List<IHoliday> GetListHolidayMoreDays(int numberOfDays)
        {
            return _holidayList.Where(h => h.getHolidaysDaysWithMoreThanXDaysOff(numberOfDays)).ToList();
        }

        public List<IHoliday> getListHolidayFilterByColaborator(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
        {
            IEnumerable<IHoliday> holidayList = _holidayList.Where(h => h.hasColaboratorAndHolidayPeriodsDuring(colaborator, startDate, endDate)); 

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


        // public List<IHolidays> getListHolidayFilterByColaborator(colaborator, startDate, endDate) {
        
        //     return _holidayList.Where(h => h.getListHolidayFilterByColaborator(colaborator, startDate, endDate).Any()).ToList();
        // }
    }
}