
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Service.Model;

namespace M2MT.Test.Shared.Service.InformationModel
{
    public class InformationModelReadServiceTest
    {
        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIInformationModelReadService()
        {
            // Arrange
            var repository = A.Fake<IInformationModelReadRepository>();
            A.CallTo(() => repository.GetModels()).Returns(new List<Model>());

            var service = new InformationModelReadService(repository);

            // Act
            var actual = (service is IInformationModelReadService);

            // Assert
            Assert.True(actual);
        }

        //public void GetModel_ModelsIsEmpty_ReturnsEmptyList()
    }
}
