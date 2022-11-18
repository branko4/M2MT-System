using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IService.InformationModel;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Service.ModelRead;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2MT.Test.Shared.Service.InformationModel
{
    public class ElementReadServiceTest : GenericReadServiceTest<ElementReadService, Element, IElementReadRepository>
    {
        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIInformationModelReadService()
        {
            BuildAUutInstance(WITH_NO_CONFIGURATION).IsInstanceOf<IElementReadService>();
        }

        protected override UUTBuilder<ElementReadService> BuildAUutInstance(MethodConfiguration<IElementReadRepository>[] conf)
        {
            return TestA
                .ObjectWithType<ElementReadService>()
                .AddBuildedParameterAs(A.Fake<IElementReadRepository>())
                .WithMethodConfiguration(conf);
        }

        protected override MethodConfiguration<IElementReadRepository> BuildConfForUutDepenedencyGetAll()
        {
            return new MethodConfiguration<IElementReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(new List<Element>());
            });
        }
    }
}
