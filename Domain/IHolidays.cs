using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IHolidays
    {
        public Holiday addHoliday(IColaborator colaborator);
    }
}