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
            Mock<IProject> project = new Mock<IProject>();
            List<Holidays> holidays = new List<Holidays>() {};
            // act
            var result = new ProjectHolidays(project.Object, holidays);
            // assert
            Assert.NotNull(result);
        }
        [Fact]
        public void WhenPassingCorrectProjectAndEmptyList_ShouldInstanciate()
        {
            // arrange
            Mock<IProject> project = new Mock<IProject>();
            List<Holidays> holidays = null;
            // act
            var result = Assert.Throws<ArgumentException>(() => new ProjectHolidays(project.Object, holidays));
            // assert
            Assert.Equal("Project or holidays cannot be null", result.Message);
        }
        [Fact]
        public void WhenPassingIncorrectProjectAndCorrectList_ShouldInstanciate()
        {
            // arrange
            Mock<IProject> project = new Mock<IProject>();
            List<Holidays> holidays = new List<Holidays>() {};
            // act
            var result = Assert.Throws<ArgumentException>(() => new ProjectHolidays(null, holidays));
            // assert
            Assert.Equal("Project or holidays cannot be null", result.Message);
        }
    }
}
