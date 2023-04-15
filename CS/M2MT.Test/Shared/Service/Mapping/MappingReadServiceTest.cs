
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
        public MappingReadServiceTest()
        {
            base.ListOfAllObjects = model.KnowMappings;
        }

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingReadService()
        {
            BuildAUutInstance().IsInstanceOf<IMappingReadService>();
        }

        protected override UUTBuilder<MappingReadService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<MappingReadService>()
                // mapping repo
                .AddBuildedParameterAs(A.Fake<IMappingReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IMappingReadRepository, MappingModel>(model.KnowMappings));
        }
    }
}
