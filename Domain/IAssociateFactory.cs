using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IAssociateFactory
    {
        Associate NewAssociate(IColaborator colaborator, DateOnly startDate, DateOnly? endDate);
    }
}