
namespace M2MT.Test.Shared.Util.GenericTest
{
    public abstract class GenericReadServiceTest<UUTType, UUTModelType, UUTDependency>
    {
        public readonly MethodConfiguration<UUTDependency>[] WITH_NO_CONFIGURATION = new MethodConfiguration<UUTDependency>[0];
        abstract protected UUTBuilder<UUTType> BuildAUutInstance(MethodConfiguration<UUTDependency>[] conf);
        abstract protected MethodConfiguration<UUTDependency> BuildConfForUutDepenedencyGetAll();

        [Fact]
        public async void GetAll_NoInstances_ReturnsEmptyList()
        {
            BuildAUutInstance(new MethodConfiguration<UUTDependency>[]
            {
                BuildConfForUutDepenedencyGetAll(),
            })
                .SoThatFunction("GetAll")
                .ReturnsAsync<IEnumerable<UUTModelType>>(new List<UUTModelType>());
        }
    }
}
