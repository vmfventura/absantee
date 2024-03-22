using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Domain
{
    public class ProjectHolidays
    {
        private IProject _project;
        private List<Holidays> _holidays = new List<Holidays>();

        public ProjectHolidays(IProject project, List<Holidays> holidays)
        {
            if (project is null || holidays is null)
            {
                throw new ArgumentException("Project or holidays cannot be null");
            }

            _project = project;
            this._holidays = holidays;
        }
    }
}