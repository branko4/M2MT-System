
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Service.ModelRead;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;

namespace M2MT.Test.Shared.Service.InformationModel
{
    public class AttributeReadServiceTest : GenericReadServiceTest<AttributeReadService, AttributeModel, IAttributeReadRepository>
    {
        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIInformationModelReadService()
        {
            BuildAUutInstance(WITH_NO_CONFIGURATION).IsInstanceOf<IAttributeReadService>();
        }

        protected override UUTBuilder<AttributeReadService> BuildAUutInstance(MethodConfiguration<IAttributeReadRepository>[] conf)
        {
            return TestA
                .ObjectWithType<AttributeReadService>()
                .AddBuildedParameterAs(A.Fake<IAttributeReadRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IAttributeReadRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IAttributeReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(new List<AttributeModel>());
            });
        }
    }
}
