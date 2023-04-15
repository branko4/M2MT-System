
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Service.Model;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;

namespace M2MT.Test.Shared.Service.InformationModel
{
    public class AttributeReadServiceTest : GenericReadServiceTest<AttributeReadService, AttributeModel, IAttributeReadRepository>
    {
        public AttributeReadServiceTest()
        {
            base.ListOfAllObjects = model.KnowAttributes;
        }

        // TODO FIXME, might be wrong implemented, now the max and mim are tested by instance of, however it would be more logical if that would be done for the know attributes
        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIInformationModelReadService()
        {
            BuildAUutInstance().IsInstanceOf<IAttributeReadService>();
        }

        protected override UUTBuilder<AttributeReadService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<AttributeReadService>()
                // attribute repo
                .AddBuildedParameterAs(A.Fake<IAttributeReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IAttributeReadRepository, AttributeModel>(model.KnowAttributes));
        }
    }
}
