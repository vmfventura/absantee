using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Domain.Tests
{
    public class ProjectHolidaysTest
    {
        [Fact]
        public void WhenPassingCorrectValues_ShouldInstanciate()
        {
            // arrange
            List<IProject> project = new List<IProject>() {};
            List<IHolidays> holidays = new List<IHolidays>() {};
            // act
            var result = new ProjectHolidays(project, holidays);
            // assert
            Assert.NotNull(result);
        }
        [Fact]
        public void WhenPassingCorrectProjectAndEmptyList_ShouldInstanciate()
        {
            // arrange
            List<IProject> project = new List<IProject>() {};
            List<IHolidays> holidays = null;
            // act
            var result = Assert.Throws<ArgumentException>(() => new ProjectHolidays(project, holidays));
            // assert
            Assert.Equal("Project or holidays cannot be null", result.Message);
        }
        [Fact]
        public void WhenPassingIncorrectProjectAndCorrectList_ShouldInstanciate()
        {
            // arrange
            List<IProject> project = null;
            List<IHolidays> holidays = new List<IHolidays>() {};
            // act
            var result = Assert.Throws<ArgumentException>(() => new ProjectHolidays(null, holidays));
            // assert
            Assert.Equal("Project or holidays cannot be null", result.Message);
        }

        [Fact]
        public void WhenPassingCorrectInformation_ShouldReturnNumberOfHolidaysDays()
        {
            // arrange
            DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 12, 01);
            DateOnly startDate2 = new DateOnly(DateTime.Now.Year, 03, 01);
            DateOnly endDate2 = new DateOnly(DateTime.Now.Year, 04, 01);

            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
            Mock<IProject> projectDouble = new Mock<IProject>();

            Mock<IHolidayPeriodFactory> holidayPeriodFactory = new Mock<IHolidayPeriodFactory>();

            Holiday holidayInstance = new Holiday(colabDouble.Object);
            holidayInstance.addHolidayPeriod(holidayPeriodFactory.Object, startDate, endDate);
            List<IHoliday> holidays = new List<IHoliday> { holidayInstance };

            Associate associate = new Associate(colabDouble.Object, startDate, endDate);
            Associate associate2 = new Associate(colabDouble.Object, startDate2, endDate2);
            List<IAssociate> associates = new List<IAssociate> { associate, associate2 };

            Mock<IHolidays> holidaysDouble = new Mock<IHolidays>();            

            projectDouble.Setup(p => p.getListByColaboratorInRange(colabDouble.Object, startDate, endDate)).Returns(associates);
            holidaysDouble.Setup(h => h.getListHolidayFilterByColaborator(colabDouble.Object, startDate, endDate)).Returns( holidays );
            holidaysDouble.Setup(h => h.getNumberOfHolidaysDaysForColaboratorDuringPeriod(colabDouble.Object, startDate, endDate)).Returns(10);

            List<IProject> projects = new List<IProject> { projectDouble.Object };
            List<IHolidays> holidaysList = new List<IHolidays> { holidaysDouble.Object };


            ProjectHolidays projectHolidays = new ProjectHolidays(projects, holidaysList);

            // act
            var result = projectHolidays.GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(colabDouble.Object, projectDouble.Object, startDate, endDate);

            // assert
            Assert.Equal(10, result);
        }

        [Fact]
        public void WhenPassingCorrectInformationButColaboratorIsNotInProject_ShouldReturnZeroOfHolidaysDays()
        {
            // arrange
            DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 12, 01);

            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
            Mock<IProject> projectDouble = new Mock<IProject>();

            Mock<IHolidayPeriodFactory> holidayPeriodFactory = new Mock<IHolidayPeriodFactory>();

            Holiday holidayInstance = new Holiday(colabDouble.Object);
            holidayInstance.addHolidayPeriod(holidayPeriodFactory.Object, startDate, endDate);
            List<IHoliday> holidays = new List<IHoliday> { holidayInstance };

            Associate associate = new Associate(colabDouble.Object, startDate, endDate);
            List<IAssociate> associates = new List<IAssociate> { }; // Colaborator not in project

            Mock<IHolidays> holidaysDouble = new Mock<IHolidays>();            

            projectDouble.Setup(p => p.getListByColaboratorInRange(colabDouble.Object, startDate, endDate)).Returns(associates);
            holidaysDouble.Setup(h => h.getListHolidayFilterByColaborator(colabDouble.Object, startDate, endDate)).Returns( holidays );
            holidaysDouble.Setup(h => h.getNumberOfHolidaysDaysForColaboratorDuringPeriod(colabDouble.Object, startDate, endDate)).Returns(10);

            List<IProject> projects = new List<IProject> { projectDouble.Object };
            List<IHolidays> holidaysList = new List<IHolidays> { holidaysDouble.Object };


            ProjectHolidays projectHolidays = new ProjectHolidays(projects, holidaysList);

            // act
            var result = projectHolidays.GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(colabDouble.Object, projectDouble.Object, startDate, endDate);

            // assert
            Assert.Equal(0, result);
        }

        [Fact]
        public void WhenPassingCorrectInformationButColaboratorWithoutHolidays_ShouldReturnZeroOfHolidaysDays()
        {
            // arrange
            DateOnly startDate = new DateOnly(DateTime.Now.Year, 01, 01);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 02, 01);


            Mock<IColaborator> colabDouble = new Mock<IColaborator>();
            Mock<IProject> projectDouble = new Mock<IProject>();

            Mock<IHolidayPeriodFactory> holidayPeriodFactory = new Mock<IHolidayPeriodFactory>();

            Holiday holidayInstance = new Holiday(colabDouble.Object);
            holidayInstance.addHolidayPeriod(holidayPeriodFactory.Object, startDate, endDate);
            List<IHoliday> holidays = new List<IHoliday> { holidayInstance };

            Associate associate = new Associate(colabDouble.Object, startDate, endDate);
            
            List<IAssociate> associates = new List<IAssociate> { associate }; 

            Mock<IHolidays> holidaysDouble = new Mock<IHolidays>();            

            projectDouble.Setup(p => p.getListByColaboratorInRange(colabDouble.Object, startDate, endDate)).Returns(associates);
            holidaysDouble.Setup(h => h.getListHolidayFilterByColaborator(colabDouble.Object, startDate, endDate)).Returns( holidays );
            holidaysDouble.Setup(h => h.getNumberOfHolidaysDaysForColaboratorDuringPeriod(colabDouble.Object, startDate, endDate)).Returns(0);

            List<IProject> projects = new List<IProject> { projectDouble.Object };
            List<IHolidays> holidaysList = new List<IHolidays> { holidaysDouble.Object };


            ProjectHolidays projectHolidays = new ProjectHolidays(projects, holidaysList);

            // act
            var result = projectHolidays.GetHolidaysDaysColaboratorInProjectDuringPeriodOfTime(colabDouble.Object, projectDouble.Object, startDate, endDate);

            // assert
            Assert.Equal(0, result);
        }

    }
}
