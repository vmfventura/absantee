using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Tests
{
    public class AssociateFactoryTest
    {
        [Fact]
        public void IfPassingValidDates_ShouldReturnANewAssociate()
        {
            // arrange
            Mock<IColaborator> colabDoube = new Mock<IColaborator>();
            DateOnly startDate = new DateOnly(DateTime.Now.Year, 1, 1);
            DateOnly endDate = new DateOnly(DateTime.Now.Year, 1, 5);

            AssociateFactory associate = new AssociateFactory();

            // act

            var result = associate.NewAssociate(colabDoube.Object, startDate, endDate);

            // assert
            Assert.IsType<Associate>(result);
        }
    }
}