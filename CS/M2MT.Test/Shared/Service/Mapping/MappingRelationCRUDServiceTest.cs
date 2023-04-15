using M2MT.Shared.Exceptions;
using M2MT.Shared.IRepository;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Test.Shared.Util;
using M2MT.Test.Shared.Util.GenericTest;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingRelationCRUDServiceTest : GenericCRUDServiceTest<MappingRelationCRUDService, MappingRelation, IMappingRelationCRUDRepository>
    {
        private TestModel model = new TestModel();
        
        public MappingRelationCRUDServiceTest()
        {
            base.ListOfAllObjects = model.KnowMappingRelations;
        }

        protected override MethodConfiguration<IMappingRelationCRUDRepository> BuildConfForUutDependencyCreate()
        {
            return new MethodConfiguration<IMappingRelationCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Create(A<MappingRelation>.Ignored)).Returns(new MappingRelation() { ID = model.MappingRelationID });
            });
        }

        protected override MethodConfiguration<IMappingRelationCRUDRepository> BuildConfForUutDependencyRemove()
        {
            return new MethodConfiguration<IMappingRelationCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetOne(A<Guid>.Ignored))
                .Returns(new MappingRelation() { ID = model.MappingRelationID, AttributeLeft = model.LeftAttributeID, AttributeRight = model.RightAttributeID });
            });
        }

        protected override MappingRelation CreateOutput()
        {
            return new MappingRelation() { 
                ID = model.MappingRelationID, 
                AttributeLeft = model.LeftAttributeID, 
                AttributeRight = model.RightAttributeID 
            };
        }

        protected override object[] CreateParams()
        {
            return new object[] { new MappingRelation() { ID = new Guid(), AttributeLeft = model.LeftAttributeID, AttributeRight = model.RightAttributeID, MappingRule = model.MappingRuleID } };
        }

        protected override MappingRelation RemoveOutput()
        {
            return this.CreateOutput();
        }

        protected override object[] RemoveParams()
        {
            return new object[] { model.MappingRelationID };
        }

        protected override UUTBuilder<MappingRelationCRUDService> BuildAUutInstance()
        {
            return TestA
                .ObjectWithType<MappingRelationCRUDService>()
                // mapping relation crud repo
                .AddBuildedParameterAs(A.Fake<IMappingRelationCRUDRepository>())
                .WithMethodConfiguration(GenericFakes.GetCRUDMethodConfiguration<IMappingRelationCRUDRepository, MappingRelation>(model.KnowMappingRelations, model.MappingRelationID))
                // mapping repo
                .AddBuildedParameterAs(A.Fake<IMappingRuleReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IMappingRuleReadRepository, MappingRule>(model.KnowMappingRules))
                // attribute repo
                .AddBuildedParameterAs(A.Fake<IAttributeReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IAttributeReadRepository, AttributeModel>(model.KnowAttributes))
                // element repo
                .AddBuildedParameterAs(A.Fake<IElementReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IElementReadRepository, Element>(model.KnowElements));
        }

        [Fact]
        // [Create] Happy flow
        public void Create_WithValidInput_ReturnsCreatedMapping()
        {
            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingRelation { ID = new Guid(), AttributeLeft = model.LeftAttributeID, AttributeRight = model.RightAttributeID, MappingRule = model.MappingRuleID } })
                .ReturnsAsync(new MappingRelation { ID = model.MappingRelationID, AttributeLeft = model.LeftAttributeID, AttributeRight = model.RightAttributeID, MappingRule = model.MappingRuleID });
        }

        [Fact]
        // [Create] Unknown mapping rule
        public void Create_WithUknownMappingRuleRef_ThrowsNotFoundException()
        {
            this.BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingRelation { ID = new Guid(), AttributeLeft = model.LeftAttributeID, AttributeRight = model.RightAttributeID, MappingRule = Guid.NewGuid() } })
                .ThrowsAsync<NotFoundException<MappingRule>, MappingRelation>();
        }

        [Fact]
        // [Create] Unkown property in system
        public void Create_WithUknownRightAttribute_ThrowsNotFoundException()
        {
            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingRelation { ID = new Guid(), AttributeLeft = model.LeftAttributeID, AttributeRight = Guid.NewGuid(), MappingRule = model.MappingRuleID } })
                .ThrowsAsync<NotFoundException<AttributeModel>, MappingRelation>();
        }

        [Theory]
        // [Create] Unkown mapping rule
        [InlineData("eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "ff74346b-4707-4ed4-8221-f34ad2348dae", new[] { "mapping rule reference referenceses to unknown mapping rule" }, new[] {typeof(NotFoundException<MappingRule>)})]
        // [Create] Invalid relation (properties are the same) && [Create] Unknown mapping rule
        [InlineData("eef28537-18b4-412f-bb30-996365d6c626", "eef28537-18b4-412f-bb30-996365d6c626", "ff74346b-4707-4ed4-8221-f34ad2348dae", new[] { "Attribute references referenceses the same attribute", "mapping rule reference referenceses to unknown mapping rule" }, new[] { typeof(NotFoundException<MappingRule>), typeof(ArgumentException) })]
        // [Create] Unkown property in system
        [InlineData("eef28537-18b4-412f-bb30-996365d6c626", "ff74346b-4707-4ed4-8221-f34ad2348dae", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "attribute reference referenceses to unknown attribute" }, new[] { typeof(NotFoundException<AttributeModel>) })]
        [InlineData("ff74346b-4707-4ed4-8221-f34ad2348dae", "eef28537-18b4-412f-bb30-996365d6c626", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "attribute reference referenceses to unknown attribute", }, new[] { typeof(NotFoundException<AttributeModel>) })]

        // [Create] Unkown property in system && [Create] Unkown mapping rule 
        [InlineData("ff74346b-4707-4ed4-8221-f34ad2348dae", "eef28537-18b4-412f-bb30-996365d6c626", "ff74346b-4707-4ed4-8221-f34ad2348dae", 
            new[] { "attribute reference referenceses to unknown attribute", "mapping rule reference referenceses to unknown mapping rule" }, 
            new[] { typeof(NotFoundException<MappingRule>), typeof(NotFoundException<AttributeModel>) })]
        // [Create] Invalid relation (properties are the same) && [Create] Unknown property in system
        [InlineData("ff74346b-4707-4ed4-8221-f34ad2348dae", "ff74346b-4707-4ed4-8221-f34ad2348dae", "3d9471a6-155b-4885-ba75-66b9680e2d50", 
            new[] { "attribute reference referenceses to unknown attribute", "Attribute references referenceses the same attribute" }, 
            new[] { typeof(NotFoundException<MappingRule>), typeof(ArgumentException) })]
        // [Create] Invalid relation (properties are the same) && [Create] Unknown mapping rule && [Create] Unknown property in system
        [InlineData("ff74346b-4707-4ed4-8221-f34ad2348dae", "ff74346b-4707-4ed4-8221-f34ad2348dae", "ff74346b-4707-4ed4-8221-f34ad2348dae", 
            new[] { "attribute reference referenceses to unknown attribute", "Attribute references referenceses the same attribute", "mapping rule reference referenceses to unknown mapping rule" }, 
            new[] { typeof(NotFoundException<MappingRule>), typeof(NotFoundException<AttributeModel>), typeof(ArgumentException) })]

        // [Create] Unkown property (not in mapping rule)
        // TODO make two elements which are bound to other model
        [InlineData(
            "eef28537-18b4-412f-bb30-996365d6c626", "a3cd41bf-199f-4432-a276-376cba3cc490", "3d9471a6-155b-4885-ba75-66b9680e2d50",
            new[] { "attribute reference referenceses to unknown attribute for given mapping rule(s)" },
            new[] { typeof(NotFoundException<AttributeModel>) })]
        [InlineData(
            "a3cd41bf-199f-4432-a276-376cba3cc490", "eef28537-18b4-412f-bb30-996365d6c626", "3d9471a6-155b-4885-ba75-66b9680e2d50",
            new[] { "attribute reference referenceses to unknown attribute for given mapping rule(s)" },
            new[] { typeof(NotFoundException<AttributeModel>) })]

        // [Create] Invalid relation (property is from same model)
        [InlineData(
            "0d9f0f2d-ca72-448a-b2de-e1b12a452ff9", "eef28537-18b4-412f-bb30-996365d6c626", "3d9471a6-155b-4885-ba75-66b9680e2d50",
            new[] { "attributes are from the same model" },
            new[] { typeof(ArgumentException) })]

        public void Create_WithUknownLeftAttribute_ThrowsNotFoundException(string leftID, string rightID, string mappingRuleID, string[] withOneOfTheseExceptionMessages, Type[] exceptionTypes)
        {
            this.model = new TestModel();

            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingRelation { ID = new Guid(), AttributeLeft = new Guid(leftID), AttributeRight = new Guid(rightID), MappingRule = new Guid(mappingRuleID) } })
                .ThrowsAsync<MappingRelation>(withOneOfTheseExceptionMessages, exceptionTypes);
        }

        [Fact]
        // [Create] Unknown property in system
        public void Create_WithUknownBothAttribute_ThrowsNotFoundException()
        {
            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingRelation { ID = new Guid(), AttributeLeft = Guid.NewGuid(), AttributeRight = Guid.NewGuid(), MappingRule = model.MappingRuleID } })
                .ThrowsAsync <NotFoundException<AttributeModel>, MappingRelation>();
        }

        private static readonly string staticID = "b500070d-61b5-4382-9d30-60aed5918acf";
        private static readonly string staticLeftID = "eef28537-18b4-412f-bb30-996365d6c626";
        private static readonly string staticRightID = "26677dba-1b71-40ee-a6b4-e61f273eb481";
        private static readonly string staticMappingRule = "3d9471a6-155b-4885-ba75-66b9680e2d50";


        [Theory]
        // General null check
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "00000000-0000-0000-0000-000000000000", "26677dba-1b71-40ee-a6b4-e61f273eb481", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute reference can not be null" })]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "00000000-0000-0000-0000-000000000000", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute reference can not be null" })]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "00000000-0000-0000-0000-000000000000", new[] { "mapping rule reference can not be null" })]
        [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "26677dba-1b71-40ee-a6b4-e61f273eb481", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute reference can not be null" })]
        // [Create] Invalid relation (properties are the same) && General null check
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute reference can not be null", "Attribute references referenceses the same attribute" })]
        // General null check
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", new[] { "Attribute reference can not be null", "mapping rule reference can not be null" })]
        [InlineData("00000000-0000-0000-0000-000000000000", "eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "00000000-0000-0000-0000-000000000000", new[] { "Attribute reference can not be null", "mapping rule reference can not be null" })]
        // [Create] Invalid relation (properties are the same) && [Create] Invalid relation (property is the same)
        [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute reference can not be null", "Attribute references referenceses the same attribute" })]
        // General null check && 
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", new[] { "Attribute reference can not be null", "mapping rule reference can not be null", "Attribute references referenceses the same attribute" })]
        // General null check
        [InlineData("00000000-0000-0000-0000-000000000000", "eef28537-18b4-412f-bb30-996365d6c626", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", new[] { "Attribute reference can not be null", "mapping rule reference can not be null" })]
        [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "26677dba-1b71-40ee-a6b4-e61f273eb481", "00000000-0000-0000-0000-000000000000", new[] { "Attribute reference can not be null", "mapping rule reference can not be null" })]
        // [Create] Invalid relation (properties are the same) && General null check
        [InlineData("00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", "00000000-0000-0000-0000-000000000000", new[] { "Attribute reference can not be null", "mapping rule reference can not be null", "Attribute references referenceses the same attribute" })]
        // [Create] Invalid relation (properties are the same)
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "eef28537-18b4-412f-bb30-996365d6c626", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute references referenceses the same attribute" })]
        // [Create] Invalid relation (properties are the same) && General null check
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "eef28537-18b4-412f-bb30-996365d6c626", "00000000-0000-0000-0000-000000000000", new[] { "Attribute references referenceses the same attribute", "mapping rule reference can not be null" })]

        // [Create] Invalid relation (properties are the same)
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute references referenceses the same attribute" })]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", new[] { "Attribute references referenceses the same attribute" })]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "3d9471a6-155b-4885-ba75-66b9680e2d50", new[] { "Attribute references referenceses the same attribute" })]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", new[] { "Attribute references referenceses the same attribute" })]
        // [Create] Invalid relation (properties are the same) && General null check
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "00000000-0000-0000-0000-000000000000", new[] { "Attribute references referenceses the same attribute", "mapping rule reference can not be null" })]
        public void Create_WithInvalidArgument_ThrowsArgumentException(string id, string leftID, string rightID, string mappingRuleID, string[] withOneOfTheseExceptionMessages)
        {
            this.model = new TestModel(staticID, staticLeftID, staticRightID, staticMappingRule);

            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingRelation { ID = new Guid(id), AttributeLeft = new Guid(leftID), AttributeRight = new Guid(rightID), MappingRule = new Guid(mappingRuleID) } })
                .ThrowsAsync<ArgumentException, MappingRelation>(withOneOfTheseExceptionMessages);
        }

        [Theory]
        // [Create] Happy flow
        [InlineData("00000000-0000-0000-0000-000000000000", "eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "3d9471a6-155b-4885-ba75-66b9680e2d50")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "3d9471a6-155b-4885-ba75-66b9680e2d50")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "26677dba-1b71-40ee-a6b4-e61f273eb481", "3d9471a6-155b-4885-ba75-66b9680e2d50")]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "ffffffff-ffff-ffff-ffff-ffffffffffff", "26677dba-1b71-40ee-a6b4-e61f273eb481", "3d9471a6-155b-4885-ba75-66b9680e2d50")]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "ffffffff-ffff-ffff-ffff-ffffffffffff", "3d9471a6-155b-4885-ba75-66b9680e2d50")]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "eef28537-18b4-412f-bb30-996365d6c626", "ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff")]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "ffffffff-ffff-ffff-ffff-ffffffffffff")]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "ffffffff-ffff-ffff-ffff-ffffffffffff", "26677dba-1b71-40ee-a6b4-e61f273eb481", "ffffffff-ffff-ffff-ffff-ffffffffffff")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "ffffffff-ffff-ffff-ffff-ffffffffffff")]
        [InlineData("ffffffff-ffff-ffff-ffff-ffffffffffff", "ffffffff-ffff-ffff-ffff-ffffffffffff", "26677dba-1b71-40ee-a6b4-e61f273eb481", "ffffffff-ffff-ffff-ffff-ffffffffffff")]
        [InlineData("b500070d-61b5-4382-9d30-60aed5918acf", "eef28537-18b4-412f-bb30-996365d6c626", "26677dba-1b71-40ee-a6b4-e61f273eb481", "3d9471a6-155b-4885-ba75-66b9680e2d50")]
        public void Create_WithValidArgument_ReturnsCreatedMapping(string id, string leftID, string rightID, string mappingRuleID)
        {
            this.model = new TestModel(id, leftID, rightID, mappingRuleID);

            BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingRelation { ID = new Guid(id), AttributeLeft = model.LeftAttributeID, AttributeRight = model.RightAttributeID, MappingRule = model.MappingRuleID } })
                .ReturnsAsync(new MappingRelation { ID = model.MappingRelationID, AttributeLeft = model.LeftAttributeID, AttributeRight = model.RightAttributeID, MappingRule = model.MappingRuleID });
        }

        [Theory]
        // [Delete] Unknown relation
        [InlineData(ValueTypes.UNKNOW,
            new[] { "mapping relation is not found" },
            new[] { typeof(NotFoundException<MappingRelation>) }
            )]
        [InlineData(ValueTypes.NULL,
            new[] { "Mapping relation reference is null" },
            new[] { typeof(ArgumentException) }
            )]
        [InlineData(ValueTypes.MAX,
            new[] { "mapping relation is not found" },
            new[] { typeof(NotFoundException<MappingRelation>) }
            )]
        public void Remove_WithInvalidMappingRef_ThrowsArgumentException(ValueTypes type, string[] withOneOfTheseExceptionMessages, Type[] exceptionTypes)
        {
            this.BuildAUutInstance()
                .SoThatFunction("Remove")
                .WithParams(new object[] { TestModel.ValueManager.GetValue<Guid>(type) })
                .ThrowsAsync<MappingRelation>(withOneOfTheseExceptionMessages, exceptionTypes);
        }

        [Theory]
        // [Delete] Happy flow
        [InlineData(ValueTypes.KNOW, false)]
        [InlineData(ValueTypes.MAX, true)]
        public void Remove_WithValidMappingRef_ReturnsDeletedObject(ValueTypes type, bool maxIsKnow)
        {
            model = new TestModel(maxIsKnow);
            this.BuildAUutInstance()
                .SoThatFunction("Remove")
                .WithParams(new object[] { TestModel.ValueManager.GetValue<Guid>(type, model.MappingRelationID) })
                .ReturnsAsync(new MappingRelation { 
                    ID = TestModel.ValueManager.GetValue(type, model.MappingRelationID), 
                    AttributeLeft = TestModel.ValueManager.GetValue(type, model.LeftAttributeID), 
                    AttributeRight = model.RightAttributeID, 
                    MappingRule = TestModel.ValueManager.GetValue(type, model.MappingRuleID)
                });
        }
    }
}
