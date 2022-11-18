using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Service.Model;
using M2MT.Test.Shared.Util.GenericTest;
using M2MT.Test.Shared.Util;
using M2MT.Shared.Model.InformationModel;

namespace M2MT.Test.Shared.Service.InformationModel
{
    public class InformationModelReadServiceTest : GenericReadServiceTest<InformationModelReadService, Model, IInformationModelReadRepository>
    {

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIInformationModelReadService()
        {
            BuildAUutInstance(WITH_NO_CONFIGURATION).IsInstanceOf<IInformationModelReadService>();
        }

        protected override UUTBuilder<InformationModelReadService> BuildAUutInstance(MethodConfiguration<IInformationModelReadRepository>[] conf)
        {
            return TestA
                .ObjectWithType<InformationModelReadService>()
                .AddBuildedParameterAs(A.Fake<IInformationModelReadRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IInformationModelReadRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IInformationModelReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetModels()).Returns(new List<Model>());
            });
        }
    }
}
