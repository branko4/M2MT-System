
using M2MT.Shared.IService;

namespace M2MT.Test.Shared.Util.GenericTest
{
    public abstract class GenericCRUDServiceTest<UUTType, UUTModelType, UUTDependency> : GenericReadServiceTest<UUTType, UUTModelType, UUTDependency>
    {
        [Obsolete]
        abstract protected object[] CreateParams();
        [Obsolete]
        abstract protected UUTModelType CreateOutput();

        [Obsolete]
        abstract protected MethodConfiguration<UUTDependency> BuildConfForUutDependencyCreate();

        [Obsolete]
        protected void Create_HappyFlow_ReturnsCreatedInstance(object[] parameters, object ExpectedOutput)
        {
            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(parameters)
                .ReturnsAsync(ExpectedOutput);
        }

        [Obsolete]
        abstract protected object[] RemoveParams();

        [Obsolete]
        abstract protected UUTModelType RemoveOutput();

        [Obsolete]
        abstract protected MethodConfiguration<UUTDependency> BuildConfForUutDependencyRemove();

        //[Fact]
        [Obsolete]
        protected void Remove_HappyFlow_ReturnsDeletedInstance()
        {
            BuildAUutInstance()
                .SoThatFunction("Remove")
                .WithParams(RemoveParams())
                .ReturnsAsync(RemoveOutput());
        }

        [Fact]
        public void Constructor_EveryThingIsDefault_IsInstanceOfICRUDService()
        {
            BuildAUutInstance().IsInstanceOf<ICRUDService<UUTModelType>>();
        }
    }
}
