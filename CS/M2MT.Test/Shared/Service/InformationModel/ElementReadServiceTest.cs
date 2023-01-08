using M2MT.Shared.Exceptions;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Service.Model;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;
using IModel = M2MT.Shared.Model.InformationModel.Model;

namespace M2MT.Test.Shared.Service.InformationModel
{
    public class ElementReadServiceTest : GenericReadServiceTest<ElementReadService, Element, IElementReadRepository>
    {
        public ElementReadServiceTest()
        {
            base.ListOfAllObjects = model.KnowElements;
        }

        // TODO/ FIXME it appears that the given name of method is incorrect, since it is not check for IInformationModelReadService, but for IElementReadService
        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIInformationModelReadService()
        {
            BuildAUutInstance().IsInstanceOf<IElementReadService>();
        }

        protected override UUTBuilder<ElementReadService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<ElementReadService>()
                // element repo
                .AddBuildedParameterAs(A.Fake<IElementReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IElementReadRepository, Element>(model.KnowElements))
                // model repo
                .AddBuildedParameterAs(A.Fake<IInformationModelReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IInformationModelReadRepository, IModel>(model.KnowModels));
        }

        protected UUTBuilder<ElementReadService> BuildANewUutInstance(bool exists, IEnumerable<Element> returnObject)
        {
            return TestA
                .ObjectWithType<ElementReadService>()
                .AddBuildedParameterAs(A.Fake<IElementReadRepository>())
                .WithMethodConfiguration(new[] { this.BuildConfigForElementRepo(returnObject) })
                .AddBuildedParameterAs(A.Fake<IInformationModelReadRepository>())
                .WithMethodConfiguration(new[] { this.BuildConfigForModelRepo(exists) });
        }

        private MethodConfiguration<IElementReadRepository> BuildConfigForElementRepo(IEnumerable<Element> returnObject)
        {
            return new MethodConfiguration<IElementReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(returnObject);
            });
        }

        private MethodConfiguration<IInformationModelReadRepository> BuildConfigForModelRepo(bool exists)
        {
            return new MethodConfiguration<IInformationModelReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Excists(A<Guid>.Ignored)).Returns(exists);
            });
        }

        private MethodConfiguration<IElementReadRepository> BuildConfigBuilderForGetAll(bool exists, IEnumerable<Element> returnObject)
        {
            return new MethodConfiguration<IElementReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAllElementsOfModel(A<Guid>.Ignored)).Returns(returnObject);
            });
        }

        [Fact]
        // [Read (All)] Happy flow
        public void GetAllElementsOfModel_CorrectGuidWichReferencesExcistingModel_ListOfElements()
        {
            BuildANewUutInstance(true, new Element[] { })
                .SoThatFunction("GetAllElementsOfModel")
                .WithParams(new object[] { Guid.NewGuid() })
                .ReturnsAsync<IEnumerable<Element>>(new Element[] { });
        }

        [Fact]
        // [Read (All)] Unknown model
        public void GetAllElementsOfModel_CorrectGuidWichReferencesNotExcistingModel_ThrowsNotFoundException()
        {
            BuildANewUutInstance(false, new Element[] { })
                .SoThatFunction("GetAllElementsOfModel")
                .WithParams(new object[] { Guid.NewGuid() })
                .ThrowsAsync<NotFoundException<Model>, IEnumerable<Element>>();
        }

        [Fact]
        // [Read (All)] Unknown model
        public void GetAllElementsOfModel_IncorrectGuid_ThrowsArgumentException()
        {
            BuildANewUutInstance(true, new Element[] { })
                .SoThatFunction("GetAllElementsOfModel")
                .WithParams(new object[] { new Guid() })
                .ThrowsAsync<ArgumentException, IEnumerable<Element>>();
        }
    }
}
