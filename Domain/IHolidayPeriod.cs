using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHolidayPeriod
    {
        public bool isStartDateIsValid(DateOnly startDate, DateOnly endDate);
        public int getNumberOfDays();
    }
}