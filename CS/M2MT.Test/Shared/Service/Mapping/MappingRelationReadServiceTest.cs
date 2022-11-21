using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingRelationReadServiceTest : GenericReadServiceTest<MappingRelationReadService, MappingRelation, IMappingRelationReadRepository>
    {

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingReadService()
        {
            BuildAUutInstance(WITH_NO_CONFIGURATION).IsInstanceOf<IMappingRelationReadService>();
        }

        protected override UUTBuilder<MappingRelationReadService> BuildAUutInstance(MethodConfiguration<IMappingRelationReadRepository>[] conf)
        {
            return TestA
                .ObjectWithType<MappingRelationReadService>()
                .AddBuildedParameterAs(A.Fake<IMappingRelationReadRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IMappingRelationReadRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IMappingRelationReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(new List<MappingRelation>());
            });
        }
    }
}
