using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHolidays
    {
        public IHoliday addHoliday(IColaborator colaborator);
        public List<IHoliday> GetListHolidayMoreDays(int numberOfDays);
        public List<IHoliday> getListHolidayFilterByColaborator(IColaborator colaborator, DateOnly startDate, DateOnly endDate);
        public int getNumberOfHolidaysDaysForColaboratorDuringPeriod(IColaborator colaborator, DateOnly startDate, DateOnly endDate);
        public int getNumberOfDaysByColaborator(IColaborator colaborator);
    }
}