using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class ProjectFactory
    {
        public Project NewProject(string strName, DateOnly dateStart, DateOnly? dateEnd)
        {
            return new Project(strName, dateStart, dateEnd);
        }
    }
}