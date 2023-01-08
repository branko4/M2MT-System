
using M2MT.Shared.Exceptions;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingRuleCRUDServiceTest : GenericCRUDServiceTest<MappingRuleCRUDService, MappingRule, IMappingRuleCRUDRepository>
    {
        private Guid ID = Guid.NewGuid();
        private Guid MappingID = Guid.NewGuid();
        private string name = "this is a name";

        public MappingRuleCRUDServiceTest()
        {
            base.ListOfAllObjects = model.KnowMappingRules;
        }

        private MethodConfiguration<IMappingRelationReadRepository>[] CreateRelationRepositoryConfig()
        {
            return new MethodConfiguration<IMappingRelationReadRepository>[]
            {
            };
        }

        protected override MethodConfiguration<IMappingRuleCRUDRepository> BuildConfForUutDependencyCreate()
        {
            return new MethodConfiguration<IMappingRuleCRUDRepository>((repoFake) =>
            {
                A.CallTo(() => repoFake.GetOne(A<Guid>.Ignored)).Returns(new MappingRule() { ID = ID, Mapping = this.MappingID, Elements = new List<RefTo<Element>>(), Name = this.name });
                return A.CallTo(() => repoFake.Create(A<MappingRule>.Ignored)).Returns(new MappingRule() { ID = ID, Mapping = this.MappingID, Elements = new List<RefTo<Element>>(), Name = this.name });
            });
        }

        protected override MethodConfiguration<IMappingRuleCRUDRepository> BuildConfForUutDependencyRemove()
        {
            return new MethodConfiguration<IMappingRuleCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Remove(A<Guid>.Ignored)).Returns(new MappingRule() { ID = ID, Mapping = this.MappingID, Elements = new List<RefTo<Element>>(), Name = this.name });
            });
        }

        protected override MappingRule CreateOutput()
        {
            return new MappingRule() { ID = ID, Mapping = this.MappingID, Elements = new List<RefTo<Element>>(), Name = this.name };
        }

        protected override object[] CreateParams()
        {
            return new object[] { new MappingRule() { ID = ID, Mapping = this.MappingID, Elements = new List<RefTo<Element>>(), Name = this.name } };
        }

        protected override MappingRule RemoveOutput()
        {
            return this.CreateOutput();
        }

        protected override object[] RemoveParams()
        {
            return new object[] { this.ID };
        }


        private TestModel model = new TestModel();

        protected override UUTBuilder<MappingRuleCRUDService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<MappingRuleCRUDService>()
                // mapping rule crud repo
                .AddBuildedParameterAs(A.Fake<IMappingRuleCRUDRepository>())
                .WithMethodConfiguration(GenericFakes.GetCRUDMethodConfiguration<IMappingRuleCRUDRepository, MappingRule>(model.KnowMappingRules, this.ID))
                // mapping relation repo
                .AddBuildedParameterAs(A.Fake<IMappingRelationReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IMappingRelationReadRepository, MappingRelation>(model.KnowMappingRelations))
                // mapping repo
                .AddBuildedParameterAs(A.Fake<IMappingReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IMappingReadRepository, MappingModel>(model.KnowMappings));
        }

        [Theory]
        // [Create] Unknown mapping
        // mapping reference incorrect
        [InlineData(
            "ff74346b-4707-4ed4-8221-f34ad2348dae", "model name",
            new[] { "mapping is not found" },
            new[] { typeof(NotFoundException<MappingModel>) })
        ]
        [InlineData(
            "00000000-0000-0000-0000-000000000000", "model name",
            new[] { "Mapping reference can not be null" },
            new[] { typeof(ArgumentException) })
        ]
        // not [Create] Unknown mapping
        // name incorrect
        [InlineData(
            "4cd05116-ab84-4e94-9427-f8d6d072b69d", "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
            new[] { "Name can not be longer then 100 characters" },
            new[] { typeof(ArgumentException) })
        ]
        // [Create] Unknown mapping
        // name && mapping reference incorrect
        [InlineData(
            "ff74346b-4707-4ed4-8221-f34ad2348dae", "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
            new[] { "Name can not be longer then 100 characters", "mapping is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<MappingModel>) })
        ]
        [InlineData(
            "00000000-0000-0000-0000-000000000000", "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
            new[] { "Name can not be longer then 100 characters", "Mapping reference can not be null" },
            new[] { typeof(ArgumentException) })
        ]
        // [Create] Unknown element (in mapping)
        // [Create] Unknown element (not excisting)
        // TODO do something with elements
        public void Create_WithInvalidInput_ThrowsAException(string mapping, string name, string[] withOneOfTheseExceptionMessages, Type[] exceptionTypes)
        {
            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new[] { new MappingRule { ID = new Guid(), Mapping = new Guid(mapping), Name = name } })
                .ThrowsAsync<MappingRule>(withOneOfTheseExceptionMessages, exceptionTypes);
        }

        [Theory]
        // [Create] Happy flow
        [InlineData("00000000-0000-0000-0000-000000000000", "valid name")]
        [InlineData("00000000-0000-0000-0000-000000000000", "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        [InlineData("00000000-0000-0000-0000-000000000000", "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "valid name")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789")]
        // TODO do something with elements

        public void Create_WithValidInput_ReturnsCreatedObject(string id, string name)
        {
            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new[] { new MappingRule { ID = new Guid(id), Mapping = model.mappingID, Name = name } })
                .ReturnsAsync(new MappingRule { ID = ID, Mapping = model.mappingID, Name = name });
        }

        [Theory]
        // [Delete] Happy flow
        [InlineData("3d9471a6-155b-4885-ba75-66b9680e2d50")]

        public void Remove_WithValidInput_ReturnsCreatedObject(string id)
        {
            BuildAUutInstance()
                .SoThatFunction("Remove")
                .WithParams(new object[] { new Guid(id) })
                .ReturnsAsync(new MappingRule { ID = model.MappingRuleID, Mapping = model.mappingID, Name = "TestMappingRule" });
        }

        [Theory]
        // [Delete] Unknown mapping rule
        [InlineData(
            "00000000-0000-0000-0000-000000000000",
            new[] { "Mapping rule reference can not be null" },
            new[] { typeof(ArgumentException) })
        ]
        [InlineData(
            "ffffffff-ffff-ffff-ffff-ffffffffffff",
            new[] { "mapping rule is not found" },
            new[] { typeof(NotFoundException<MappingRule>) })
        ]
        [InlineData(
            "ff74346b-4707-4ed4-8221-f34ad2348dae",
            new[] { "mapping rule is not found" },
            new[] { typeof(NotFoundException<MappingRule>) })
        ]
        public void Remove_WithInvalidInput_ThrowsAException(string id, string[] withOneOfTheseExceptionMessages, Type[] exceptionTypes)
        {
            BuildAUutInstance()
                .SoThatFunction("Remove")
                .WithParams(new object[] { new Guid(id) })
                .ThrowsAsync<MappingRule>(withOneOfTheseExceptionMessages, exceptionTypes);
        }
    }
}
