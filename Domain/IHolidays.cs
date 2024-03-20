using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHolidays
    {
        public Holiday addHoliday(IColaborator colaborator);
        public List<Holiday> GetListHolidayMoreDays(int numberOfDays);
        public List<Holiday> getListHolidayFilterByColaborator(IColaborator colaborator, DateOnly startDate, DateOnly endDate);
        public int getNumberOfHolidaysDaysForColaboratorDuringPeriod(IColaborator colaborator, DateOnly startDate, DateOnly endDate);
        public int getNumberOfDaysByColaborator(IColaborator colaborator);
    }
}