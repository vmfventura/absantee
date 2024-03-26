using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Domain
{
    public class ProjectHolidays
    {
        private List<IProject> _projects = new List<IProject>();
        private List<IHolidays> _holidays = new List<IHolidays>();

        public ProjectHolidays(List<IProject> projects, List<IHolidays> holidays)
        {
            if (projects is null || holidays is null)
            {
                throw new ArgumentException("Project or holidays cannot be null");
            }

            this._projects = projects;
            this._holidays = holidays;
        }

        public int GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(IColaborator colaborator, IProject project, DateOnly startDate, DateOnly endDate)
        {
            bool projects = _projects.Where(p => p.getListByColaboratorInRange(colaborator, startDate, endDate).Any() && project.Equals(p)).Any();
            if (projects)
            {
                List<IHolidays> holidays = _holidays.Where(h => h.getListHolidayFilterByColaborator(colaborator, startDate, endDate).Any()).ToList();

                return holidays.Sum(x => x.getNumberOfHolidaysDaysForColaboratorDuringPeriod(colaborator, startDate, endDate));
            }
            else
            {
                return 0;
            }
        }

        public int GetAllHolidaysDaysColaboratorInProjectDuringPeriodOfTime(IProject project, DateOnly startDate, DateOnly endDate)
        {
            List<IColaborator> allColaborators = _projects.Where(p => p.getListColaboratorByProject().Any())
                                                                    .SelectMany(p => p.getListColaboratorByProject())
                                                                    .Distinct()
                                                                    .ToList();

            return allColaborators.Sum(colaborator => 
                    GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(colaborator, project, startDate, endDate));
        }
    }
}