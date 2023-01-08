using M2MT.Shared.Model;
using M2MT.Shared.Model.InformationModel;
using M2MT.Shared.Model.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2MT.Test.Shared.Util

{
    public enum ValueTypes
    {
        NULL = 0,
        MAX = 1,
        UNKNOW = 2,
        KNOW = 3,
    }

    public class TestModel
    {

        // model
        public readonly Guid ModelLeftID = new Guid("90e3df23-bfa9-48b5-9457-12e079ed6476");
        public readonly Guid ModelRightID = new Guid("3679e8ba-438c-4824-ab74-1e412ed5e01a");
        public readonly Guid ModelLeftForCreateID = new Guid("b0d62cbc-327f-4a9b-98c2-d1b049f08443");
        public readonly Guid ModelRightForCreateID = new Guid("4bdc4922-4118-464f-924e-22a1ae249709");

        // element
        public readonly Guid ElementLeftID = new Guid("574b694e-b14b-4797-9958-11ae9881a6d7");
        public readonly Guid ElementRightID = new Guid("b26fff8d-d2ad-4e91-875a-4461b6a9887d");
        public readonly Guid ElementLeftForCreateID = new Guid("f93759a1-1249-462d-995c-f3df3250878f");
        public readonly Guid ElementRightForCreateID = new Guid("00846508-3874-4f66-86fe-a1c831113347");

        // attribute
        public readonly Guid LeftAttributeID = new Guid("eef28537-18b4-412f-bb30-996365d6c626");
        public readonly Guid LeftAttribute2ID = new Guid("0d9f0f2d-ca72-448a-b2de-e1b12a452ff9");
        public readonly Guid RightAttributeID = new Guid("26677dba-1b71-40ee-a6b4-e61f273eb481");

        // mapping
        public readonly Guid mappingID = new Guid("4cd05116-ab84-4e94-9427-f8d6d072b69d");

        // mappingRule
        public readonly Guid MappingRuleID = new Guid("3d9471a6-155b-4885-ba75-66b9680e2d50");

        // mappingRelation
        public readonly Guid MappingRelationID = new Guid("b500070d-61b5-4382-9d30-60aed5918acf");

        // know model lists
        public readonly List<Model> KnowModels = new List<Model>();
        public readonly List<Element> KnowElements = new List<Element>();
        public readonly List<AttributeModel> KnowAttributes = new List<AttributeModel>();

        // know mapping lists
        public readonly List<MappingModel> KnowMappings = new List<MappingModel>();
        public readonly List<MappingRule> KnowMappingRules = new List<MappingRule>();
        public readonly List<MappingRelation> KnowMappingRelations = new List<MappingRelation>();

        public static readonly ValueManager ValueManager = new ValueManager();

        public TestModel()
        {
            this.buildReferences();
        }

        public TestModel(bool MaxIDIsKnow)
        {
            this.buildReferences();
            if (MaxIDIsKnow) this.buildAndAddMaxIDReferences();
        }

        public TestModel(string id, string left, string right, string mappingRule)
        {
            this.MappingRelationID = new Guid(id);
            this.LeftAttributeID = new Guid(left);
            this.RightAttributeID = new Guid(right);
            this.MappingRuleID = new Guid(mappingRule);
            this.buildReferences();
        }

        private void buildAndAddMaxIDReferences()
        {
            // create
            var model = new Model { ID = ValueManager.MAX_ID, Name = "test2", Source = "TestEnv" };
            var element = new Element { ID = ValueManager.MAX_ID, Model = ValueManager.MAX_ID, Name = "ElementA-ofB" };
            var attribute= new AttributeModel { ID = ValueManager.MAX_ID, Element = ValueManager.MAX_ID, Name = "AttributeA-ofB" };
            var mapping = new MappingModel { ID = ValueManager.MAX_ID, modelFrom = ValueManager.MAX_ID, modelTo = KnowModels.FirstOrDefault(new Model { ID = Guid.NewGuid(), Name = "test3", Source="TestEnv"}).ID, Name = "TestMapping" };
            var mappingrule = new MappingRule { ID = ValueManager.MAX_ID, Elements = new[] { new RefTo<Element> { ID = element.ID } }, Mapping = ValueManager.MAX_ID, Name = "TestMappingRule" };
            var mappingRelation = new MappingRelation { ID = ValueManager.MAX_ID, AttributeLeft = ValueManager.MAX_ID, AttributeRight = KnowAttributes.FirstOrDefault(new AttributeModel { ID = Guid.NewGuid() }).ID, MappingRule = ValueManager.MAX_ID };

            // save
            KnowModels.Add(model);
            KnowElements.Add(element);
            KnowAttributes.Add(attribute);
            KnowMappings.Add(mapping);
            KnowMappingRules.Add(mappingrule);
            KnowMappingRelations.Add(mappingRelation);
        }

        private void buildReferences()
        {
            // create models
            var modelA = new Model { ID = ModelLeftID, Name = "ModelA", Source = "TestEnv" };
            var modelB = new Model { ID = ModelRightID, Name = "ModelB", Source = "TestEnv" };
            var modelC = new Model { ID = Guid.NewGuid(), Name = "ModelC", Source = "TestEnv" };
            var modelD = new Model { ID = Guid.NewGuid(), Name = "ModelD", Source = "TestEnv" };

            // create elements
            var elementOfA = new Element { ID = ElementLeftID, Model = modelA.ID, Name = "ElementA-ofB" };
            var elementOfB = new Element { ID = ElementRightID, Model = modelB.ID, Name = "ElementA-ofB" };
            var elementBOfA = new Element { ID = Guid.NewGuid(), Model = modelA.ID, Name = "ElementB-ofB" };
            var elementBOfB = new Element { ID = Guid.NewGuid(), Model = modelB.ID, Name = "ElementB-ofB" };
            var elementAOfC = new Element { ID = Guid.NewGuid(), Model = modelC.ID, Name = "ElementA-ofC" };
            var elementAOfD = new Element { ID = Guid.NewGuid(), Model = modelD.ID, Name = "ElementA-ofD" };

            // create attributes
            //var attrAOfA = new AttributeModel { ID = new Guid("eef28537-18b4-412f-bb30-996365d6c626"), Element = elementOfA.ID, Name = "AttributeA-ofB" };
            var attrAOfA = new AttributeModel { ID = this.LeftAttributeID, Element = elementOfA.ID, Name = "AttributeA-ofB" };
            var attrBOfA = new AttributeModel { ID = this.LeftAttribute2ID, Element = elementOfA.ID, Name = "AttributeB-ofA" };
            //var attrBOfA = new AttributeModel { ID = this.RightID, Element = elementOfB.ID, Name = "AttributeB-ofA" };
            //var attrAOfB = new AttributeModel { ID = new Guid("26677dba-1b71-40ee-a6b4-e61f273eb481"), Element = elementOfB.ID, Name = "AttributeA-ofB" };
            var attrAOfB = new AttributeModel { ID = this.RightAttributeID, Element = elementOfB.ID, Name = "AttributeA-ofB" };

            var attrAOfBOfA = new AttributeModel { ID = new Guid("0b86743a-3cad-47c0-99bf-02817e9192ce"), Element = elementBOfA.ID, Name = "AttributeB-ofA" };
            var attrAOfBOfB = new AttributeModel { ID = new Guid("9cfeda0a-327e-4144-9fc6-d40f20d32905"), Element = elementBOfB.ID, Name = "AttributeB-ofA" };
            var attrAOfAOfC = new AttributeModel { ID = new Guid("f3be3de3-dcad-445d-80d4-5942844014fe"), Element = elementAOfC.ID, Name = "AttributeB-ofA" };
            var attrAOfAOfD = new AttributeModel { ID = new Guid("a3cd41bf-199f-4432-a276-376cba3cc490"), Element = elementAOfD.ID, Name = "AttributeB-ofA" };

            // add attributes to right elements
            elementOfA.Attributes = new[] { attrAOfA, attrBOfA };
            elementOfB.Attributes = new[] { attrAOfB };
            elementBOfA.Attributes = new[] { attrAOfBOfA };
            elementBOfB.Attributes = new[] { attrAOfBOfB };
            elementAOfC.Attributes = new[] { attrAOfAOfC };
            elementAOfD.Attributes = new[] { attrAOfAOfD };

            // create mapping
            var mapping = new MappingModel { ID = this.mappingID, modelFrom = modelA.ID, modelTo = modelB.ID, Name = "TestMapping" };

            // create mappingrule
            var mappingrule = new MappingRule { ID = this.MappingRuleID, Elements = new[] { new RefTo<Element> { ID = elementOfA.ID }, new RefTo<Element> { ID = elementOfB.ID } }, Mapping = mapping.ID, Name = "TestMappingRule" };

            // create mappingrelation
            var mappingRelation = new MappingRelation { 
                ID = MappingRelationID, AttributeLeft = attrAOfA.ID, AttributeRight = attrAOfB.ID, MappingRule = mappingrule.ID };

            this.KnowAttributes.AddRange(new[] { attrAOfA, attrAOfB, attrBOfA, attrAOfBOfA, attrAOfBOfB, attrAOfAOfC, attrAOfAOfD });
            this.KnowElements.AddRange(new[] { elementOfA, elementOfB, elementAOfC, elementAOfD, elementBOfA, elementBOfB });
            this.KnowModels.AddRange(new[] { modelA, modelB, modelC, modelD });
            this.KnowMappings.Add(mapping);
            this.KnowMappingRules.Add(mappingrule);
            this.KnowMappingRelations.Add(mappingRelation);
        }

        public static IEnumerable<Guid> BaseToGuid<T>(IEnumerable<T> bases) where T : Base
        {
            var list = new List<Guid>();
            foreach (var baseObject in bases)
            {
                list.Add(baseObject.ID);
            }

            return list;
        }
    }

    public class ValueManager
    {
        public static readonly Guid UNKNOW_ID = new Guid("ff74346b-4707-4ed4-8221-f34ad2348dae");
        public static readonly Guid MAX_ID = new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff");
        public static readonly Guid NULL_ID = new Guid();

        private readonly Dictionary<Type, object[]> valuesByType = new Dictionary<Type, object[]>();

        public ValueManager()
        {
            buildValues();
        }

        public T GetValue<T>(ValueTypes type, T defaultReturn)
        {
            if (ValueTypes.KNOW.Equals(type)) return defaultReturn;
            return (T)valuesByType[typeof(T)][(int)type];
        }

        public T GetValue<T>(ValueTypes type)
        {
            if (ValueTypes.KNOW.Equals(type)) throw new ArgumentException("KNOW value type is not allowed in method GetValue, when there is no default return type given");
            return (T)valuesByType[typeof(T)][(int)type];
        }

        private void buildValues()
        {
            var a = new object[3];
            a[((int)ValueTypes.NULL)] = NULL_ID;
            a[((int)ValueTypes.UNKNOW)] = UNKNOW_ID;
            a[((int)ValueTypes.MAX)] = MAX_ID;
            valuesByType.Add(typeof(Guid), a);

            var b = new object[3];
            b[((int)ValueTypes.NULL)] = null;
            b[((int)ValueTypes.UNKNOW)] = "";
            b[((int)ValueTypes.MAX)] = new String('*', 100);
            valuesByType.Add(typeof(string), b);
        }
    }
}
