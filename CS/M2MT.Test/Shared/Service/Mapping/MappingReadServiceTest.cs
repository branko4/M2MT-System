
using M2MT.Test.Shared.Util.GenericTest;
using M2MT.Test.Shared.Util;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Shared.IService.Mapping;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingReadServiceTest : GenericReadServiceTest<MappingReadService, MappingModel, IMappingReadRepository>
    {

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingReadService()
        {
            BuildAUutInstance(WITH_NO_CONFIGURATION).IsInstanceOf<IMappingReadService>();
        }

        protected override UUTBuilder<MappingReadService> BuildAUutInstance(MethodConfiguration<IMappingReadRepository>[] conf)
        {
            return TestA
                .ObjectWithType<MappingReadService>()
                .AddBuildedParameterAs(A.Fake<IMappingReadRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IMappingReadRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IMappingReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetMappings()).Returns(new List<MappingModel>());
            });
        }
    }
}
