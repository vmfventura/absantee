using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Projects : IProjects
    {
        private IProjectFactory _projectFactory;
        private List<IProject> _projectList = new List<IProject>();
        
        protected Projects() {}
        public Projects(IProjectFactory pFactory)
        {
            if (pFactory is not null)
            {
                _projectFactory = pFactory;
            }
            else
            {
                throw new ArgumentException("Project Factory cannot be null");
            }
        }

        public IProject addProject(string strName, DateOnly dateStart, DateOnly? dateEnd)
        {
            IProject project = _projectFactory.NewProject(strName, dateStart, dateEnd);
            _projectList.Add(project);
            return project;
        }

        public bool GetColaboratorInProjectDuringPeriodOfTime(IColaborator colaborator, IProject project, DateOnly startDate, DateOnly endDate)
        {
            bool isInProjects = _projectList.Where(p => p.getListByColaboratorInRange(colaborator, startDate, endDate).Any() && project.Equals(p)).Any();
            return isInProjects;
        }
    }
}