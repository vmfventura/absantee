using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class HolidaysFactory
    {
        public Holidays NewHoliday(List<Holiday> holidayList)
        {
            return new Holidays(holidayList);
        }
    }
}