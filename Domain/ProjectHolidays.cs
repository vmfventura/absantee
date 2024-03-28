using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Domain
{
    public class ProjectHolidays
    {
        private IProjects _projectObj;
        private List<IProjects> _projects = new List<IProjects>();

        private IHolidays _holidaysObj;
        private List<IHolidays> _holidays = new List<IHolidays>();

        public ProjectHolidays(IProjects projects, IHolidays holidays)
        {
            _holidaysObj = holidays;
            _projectObj = projects;
        }

        public ProjectHolidays(List<IProjects> projects, List<IHolidays> holidays)
        {
            if (projects is null || holidays is null)
            {
                throw new ArgumentException("Project or holidays cannot be null");
            }

            this._projects = projects;
            this._holidays = holidays;
        }

        // public int GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(IColaborator colaborator, IProject project, DateOnly startDate, DateOnly endDate)
        // {
            
        //     if (projects)
        //     {
        //         List<IHoliday> holidays = _holidaysObj.getListHolidayFilterByColaborator(colaborator, startDate, endDate);

        //         return holidays.Sum(x => x.getNumberOfHolidaysDaysForColaboratorDuringPeriod(colaborator, startDate, endDate));
        //     }
        //     return 0;
        // }

        // public int GetAllHolidaysDaysColaboratorInProjectDuringPeriodOfTime(IProject project, DateOnly startDate, DateOnly endDate)
        // {
        //     List<IColaborator> allColaborators = _projects.Where(p => p.getListColaboratorByProject().Any())
        //                                                             .SelectMany(p => p.getListColaboratorByProject())
        //                                                             .Distinct()
        //                                                             .ToList();

        //     return allColaborators.Sum(colaborator => 
        //             GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(colaborator, project, startDate, endDate));
        // }
    }
}