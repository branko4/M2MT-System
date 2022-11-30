using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingRelationCRUDServiceTest : GenericCRUDServiceTest<MappingRelationCRUDService, MappingRelation, IMappingRelationCRUDRepository>
    {
        private Guid ID = Guid.NewGuid();
        private Guid LeftID = Guid.NewGuid();
        private Guid RightID = Guid.NewGuid();

        protected override UUTBuilder<MappingRelationCRUDService> BuildAUutInstance(MethodConfiguration<IMappingRelationCRUDRepository>[] conf)
        {
            return TestA
                .ObjectWithType<MappingRelationCRUDService>()
                .AddBuildedParameterAs(A.Fake<IMappingRelationCRUDRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IMappingRelationCRUDRepository> BuildConfForUutDependencyCreate()
        {
            return new MethodConfiguration<IMappingRelationCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Create(A<MappingRelation>.Ignored)).Returns(new MappingRelation() { ID = ID });
            });
        }

        protected override MethodConfiguration<IMappingRelationCRUDRepository> BuildConfForUutDependencyRemove()
        {
            return new MethodConfiguration<IMappingRelationCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetOne(A<Guid>.Ignored)).Returns(new MappingRelation() { ID = ID, AttributeLeft = this.LeftID, AttributeRight = this.RightID });
            });
        }

        protected override MethodConfiguration<IMappingRelationCRUDRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IMappingRelationCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(new List<MappingRelation>());
            });
        }

        protected override MappingRelation CreateOutput()
        {
            return new MappingRelation() { ID = this.ID, AttributeLeft = this.LeftID, AttributeRight = this.RightID };
        }

        protected override object[] CreateParams()
        {
            return new object[] { new MappingRelation() };
        }

        protected override MappingRelation RemoveOutput()
        {
            return this.CreateOutput();
        }

        protected override object[] RemoveParams()
        {
            return new object[] { this.ID };
        }
    }
}
