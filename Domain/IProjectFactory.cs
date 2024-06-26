using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public interface IProjectFactory
    {
        public Project NewProject(string strName, DateOnly dateStart, DateOnly? dateEnd);
    }
}