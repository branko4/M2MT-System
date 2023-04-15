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
        public MappingRelationReadServiceTest()
        {
            base.ListOfAllObjects = model.KnowMappingRelations;
        }

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingReadService()
        {
            BuildAUutInstance().IsInstanceOf<IMappingRelationReadService>();
        }

        protected override UUTBuilder<MappingRelationReadService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<MappingRelationReadService>()
                // mapping relation repo
                .AddBuildedParameterAs(A.Fake<IMappingRelationReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IMappingRelationReadRepository, MappingRelation>(model.KnowMappingRelations));
        }
    }
}
