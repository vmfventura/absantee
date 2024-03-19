using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class HolidayFactory
    {
        public Holiday NewHoliday(IColaborator colaborator)
        {
            return new Holiday(colaborator);
        }
    }
}