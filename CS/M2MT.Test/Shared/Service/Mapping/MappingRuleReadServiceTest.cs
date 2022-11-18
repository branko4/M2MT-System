using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingRuleReadServiceTest : GenericReadServiceTest<MappingRuleReadService, MappingRule, IMappingRuleReadRepository>
    {

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingReadService()
        {
            BuildAUutInstance(WITH_NO_CONFIGURATION).IsInstanceOf<IMappingRuleReadService>();
        }

        protected override UUTBuilder<MappingRuleReadService> BuildAUutInstance(MethodConfiguration<IMappingRuleReadRepository>[] conf)
        {
            return TestA
                .ObjectWithType<MappingRuleReadService>()
                .AddBuildedParameterAs(A.Fake<IMappingRuleReadRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IMappingRuleReadRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IMappingRuleReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetMappingRules()).Returns(new List<MappingRule>());
            });
        }
    }
}
