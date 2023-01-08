using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Service.Model;
using M2MT.Test.Shared.Util.GenericTest;
using M2MT.Test.Shared.Util;
using M2MT.Shared.Model.InformationModel;
using IModel = M2MT.Shared.Model.InformationModel.Model;

namespace M2MT.Test.Shared.Service.InformationModel
{
    public class InformationModelReadServiceTest : GenericReadServiceTest<InformationModelReadService, Model, IInformationModelReadRepository>
    {
        public InformationModelReadServiceTest()
        {
            base.ListOfAllObjects = model.KnowModels;
        }

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIInformationModelReadService()
        {
            BuildAUutInstance().IsInstanceOf<IInformationModelReadService>();
        }

        protected override UUTBuilder<InformationModelReadService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<InformationModelReadService>()
                // model repo
                .AddBuildedParameterAs(A.Fake<IInformationModelReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IInformationModelReadRepository, IModel>(model.KnowModels));
        }
    }
}
