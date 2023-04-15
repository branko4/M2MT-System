
using M2MT.Shared.IService;

namespace M2MT.Test.Shared.Util.GenericTest
{
    public abstract class GenericReadServiceTest<UUTType, UUTModelType, UUTDependency>
    {
        public readonly MethodConfiguration<UUTDependency>[] WITH_NO_CONFIGURATION = new MethodConfiguration<UUTDependency>[0];
        abstract protected UUTBuilder<UUTType> BuildAUutInstance();
        protected IEnumerable<UUTModelType> ListOfAllObjects;

        protected TestModel model = new TestModel();

        [Fact]
        public async void GetAll_NoInstances_ReturnsEmptyList()
        {
            BuildAUutInstance()
                .SoThatFunction("GetAll")
                .ReturnsAsync(ListOfAllObjects);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void Constructor_EveryThingIsDefault_IsInstanceOfIReadService(bool maxIsKnow)
        {
            model = new TestModel(maxIsKnow);
            BuildAUutInstance().IsInstanceOf<IReadService<UUTModelType>>();
        }
    }
}
