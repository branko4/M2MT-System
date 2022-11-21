
using M2MT.Test.Shared.Util.GenericTest;
using M2MT.Test.Shared.Util;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.IRepository.Mapping;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingCRUDServiceTest : GenericCRUDServiceTest<MappingCRUDService, MappingModel, IMappingCRUDRepository>
    {
        // to make sure that all classes, that require the same ID, can have the same ID 
        // It would be better to have the Guid hardcoded, so each test uses the same UUID, but for now good enough
        private Guid ID = Guid.NewGuid();

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingCRUDService()
        {
            BuildAUutInstance(WITH_NO_CONFIGURATION).IsInstanceOf<IMappingCRUDService>();
        }

        protected override MappingModel CreateOutput()
        {
            return new MappingModel() { ID = this.ID };
        }

        protected override object[] CreateParams()
        {
            return new object[] { new MappingModel() };
        }

        protected override MappingModel RemoveOutput()
        {
            return this.CreateOutput();
        }

        protected override object[] RemoveParams()
        {
            return this.CreateParams();
        }

        protected override MethodConfiguration<IMappingCRUDRepository> BuildConfForUutDependencyCreate()
        {
            return new MethodConfiguration<IMappingCRUDRepository>((repoFake) =>
                {
                    return A.CallTo(() => repoFake.Create(A<MappingModel>.Ignored)).Returns(new MappingModel() { ID = this.ID });
                });
        }

        protected override MethodConfiguration<IMappingCRUDRepository> BuildConfForUutDependencyRemove()
        {
            return new MethodConfiguration<IMappingCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Remove(A<MappingModel>.Ignored)).Returns(new MappingModel() { ID = this.ID });
            });
        }

        protected override MethodConfiguration<IMappingCRUDRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IMappingCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(new List<MappingModel>());
            });
        }

        protected override UUTBuilder<MappingCRUDService> BuildAUutInstance(MethodConfiguration<IMappingCRUDRepository>[] conf)
        {
            return TestA
                .ObjectWithType<MappingCRUDService>()
                .AddBuildedParameterAs(A.Fake<IMappingCRUDRepository>())
                .WithMethodConfiguration(conf);
        }
    }
}
