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
        public MappingRuleReadServiceTest()
        {
            base.ListOfAllObjects = model.KnowMappingRules;
        }

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingReadService()
        {
            BuildAUutInstance().IsInstanceOf<IMappingRuleReadService>();
        }

        protected override UUTBuilder<MappingRuleReadService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<MappingRuleReadService>()
                // mapping repo
                .AddBuildedParameterAs(A.Fake<IMappingRuleReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IMappingRuleReadRepository, MappingRule>(model.KnowMappingRules))
                // mapping relation crud repo
                .AddBuildedParameterAs(A.Fake<IMappingRelationReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IMappingRelationReadRepository, MappingRelation>(model.KnowMappingRelations));
        }
    }
}
