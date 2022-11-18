
namespace M2MT.Test.Shared.Util.GenericTest
{
    public abstract class GenericCRUDServiceTest<UUTType, UUTModelType, UUTDependency> : GenericReadServiceTest<UUTType, UUTModelType, UUTDependency>
    {
        abstract protected object[] CreateParams();
        abstract protected UUTModelType CreateOutput();

        abstract protected MethodConfiguration<UUTDependency> BuildConfForUutDependencyCreate();

        [Fact]
        public void Create_HappyFlow_ReturnsCreatedInstance()
        {
            BuildAUutInstance(new MethodConfiguration<UUTDependency>[]
            {
                BuildConfForUutDependencyCreate(),
            })
                .SoThatFunction("Create")
                .WithParams(CreateParams())
                .ReturnsAsync(CreateOutput());
        }

        abstract protected object[] RemoveParams();
        abstract protected UUTModelType RemoveOutput();

        abstract protected MethodConfiguration<UUTDependency> BuildConfForUutDependencyRemove();

        [Fact]
        public void Remove_HappyFlow_ReturnsDeletedInstance()
        {
            BuildAUutInstance(new MethodConfiguration<UUTDependency>[]
            {
                BuildConfForUutDependencyRemove(),
            })
                .SoThatFunction("Remove")
                .WithParams(RemoveParams())
                .ReturnsAsync(RemoveOutput());
        }
    }
}
