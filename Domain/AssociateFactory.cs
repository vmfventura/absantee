using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class AssociateFactory
    {
        public Associate NewAssociate(IColaborator colaborator, DateOnly startDate, DateOnly endDate)
        {
            return new Associate(colaborator, startDate, endDate);
        }
    }
}