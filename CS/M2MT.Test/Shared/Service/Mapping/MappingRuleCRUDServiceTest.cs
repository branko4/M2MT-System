
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingRuleCRUDServiceTest : GenericCRUDServiceTest<MappingRuleCRUDService, MappingRule, IMappingRuleCRUDRepository>
    {
        private Guid ID = Guid.NewGuid();

        protected override UUTBuilder<MappingRuleCRUDService> BuildAUutInstance(MethodConfiguration<IMappingRuleCRUDRepository>[] conf)
        {
            return TestA
                .ObjectWithType<MappingRuleCRUDService>()
                .AddBuildedParameterAs(A.Fake<IMappingRuleCRUDRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IMappingRuleCRUDRepository> BuildConfForUutDependencyCreate()
        {
            return new MethodConfiguration<IMappingRuleCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Create(A<MappingRule>.Ignored)).Returns(new MappingRule() { ID = ID});
            });
        }

        protected override MethodConfiguration<IMappingRuleCRUDRepository> BuildConfForUutDependencyRemove()
        {
            return new MethodConfiguration<IMappingRuleCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Remove(A<MappingRule>.Ignored)).Returns(new MappingRule() { ID = ID });
            });
        }

        protected override MethodConfiguration<IMappingRuleCRUDRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IMappingRuleCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(new List<MappingRule>());
            });
        }

        protected override MappingRule CreateOutput()
        {
            return new MappingRule() { ID = this.ID };
        }

        protected override object[] CreateParams()
        {
            return new object[] { new MappingRule() };
        }

        protected override MappingRule RemoveOutput()
        {
            return this.CreateOutput();
        }

        protected override object[] RemoveParams()
        {
            return this.CreateParams();
        }
    }
}
