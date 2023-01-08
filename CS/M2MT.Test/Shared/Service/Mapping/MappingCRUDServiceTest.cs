
using M2MT.Test.Shared.Util.GenericTest;
using M2MT.Test.Shared.Util;
using M2MT.Shared.Model.Mapping;
using M2MT.Shared.Service.Mapping;
using M2MT.Shared.IService.Mapping;
using M2MT.Shared.IRepository.Mapping;
using M2MT.Shared.IRepository.InformationModel;
using M2MT.Shared.Exceptions;
using M2MT.Shared.Model.InformationModel;
using IModel = M2MT.Shared.Model.InformationModel.Model;

namespace M2MT.Test.Shared.Service.Mapping
{
    public class MappingCRUDServiceTest : GenericCRUDServiceTest<MappingCRUDService, MappingModel, IMappingCRUDRepository>
    {
        // to make sure that all classes, that require the same ID, can have the same ID 
        // It would be better to have the Guid hardcoded, so each test uses the same UUID, but for now good enough
        private Guid ID = Guid.NewGuid();
        private Guid ModelFromRef = Guid.NewGuid();
        private Guid ModelToRef = Guid.NewGuid();

        public MappingCRUDServiceTest()
        {
            base.ListOfAllObjects = model.KnowMappings;
        }

        [Fact]
        public void Constructor_RepositoryIsFake_IsInstanceOfIMappingCRUDService()
        {
            BuildAUutInstance().IsInstanceOf<IMappingCRUDService>();
        }

        protected override MappingModel CreateOutput()
        {
            return new MappingModel() { ID = ID, modelFrom = ModelFromRef, modelTo = ModelToRef, Name = "test" };
        }

        protected override object[] CreateParams()
        {
            return new object[] { new MappingModel() { ID = new Guid(), modelFrom = ModelFromRef, modelTo = ModelToRef, Name = "test" } };
        }

        protected override MappingModel RemoveOutput()
        {
            return this.CreateOutput();
        }

        protected override object[] RemoveParams()
        {
            return new object[] { this.ID };
        }

        protected override MethodConfiguration<IMappingCRUDRepository> BuildConfForUutDependencyCreate()
        {
            return new MethodConfiguration<IMappingCRUDRepository>((repoFake) =>
                {
                    return A.CallTo(() => repoFake.Create(A<MappingModel>.Ignored)).Returns(new MappingModel() { ID = this.ID });
                });
        }

        protected override MethodConfiguration<IMappingCRUDRepository> BuildConfForUutDependencyRemove()
        {
            return new MethodConfiguration<IMappingCRUDRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Remove(A<Guid>.Ignored)).Returns(new MappingModel() { ID = this.ID });
            });
        }

        protected override UUTBuilder<MappingCRUDService> BuildAUutInstance() {
            return TestA
                .ObjectWithType<MappingCRUDService>()
                // mapping relation crud repo
                .AddBuildedParameterAs(A.Fake<IMappingCRUDRepository>())
                .WithMethodConfiguration(GenericFakes.GetCRUDMethodConfiguration<IMappingCRUDRepository, MappingModel>(model.KnowMappings, model.mappingID))
                // mapping repo
                .AddBuildedParameterAs(A.Fake<IInformationModelReadRepository>())
                .WithMethodConfiguration(GenericFakes.GetReadMethodConfiguration<IInformationModelReadRepository, IModel>(model.KnowModels));
        }

        private MethodConfiguration<IMappingCRUDRepository> BuildConfigForMappingRepo() {
            
            return new MethodConfiguration<IMappingCRUDRepository>((repoFake) =>
            {
                
                return A.CallTo(() => repoFake.Create(A<MappingModel>.Ignored)).ReturnsLazily(x => Task.Run(async () => {
                    await Task.Delay(10);
                    var a = x.GetArgument<MappingModel>(0);
                    // Since there would not be another way to test this, it must be done this way
                    // The ID cannot be null (0000-0000...), so the given value is overwriten. However, this means that we do not have acces to the actual ID value.
                    // By checking if the ID is not null we are informaly testing it, by replacing it with a value we do know we can test the entire object.
                    // To add some proof; ID == null -> we do nothing ^ ID != null -> we change the value
                    if (!new Guid().Equals(a.ID)) a.ID = this.ID; 
                    return a;
                }));
            });
        }

        private MethodConfiguration<IMappingCRUDRepository> BuildConfigForExcistOfMappingRepo() {
            return new MethodConfiguration<IMappingCRUDRepository>((repoFake) =>
            {

                return A.CallTo(() => repoFake.Excists(A<Guid>.Ignored))
                .ReturnsLazily(x =>
                    Task.Run(async () =>
                        {
                            await Task.Delay(10);
                            return ID.Equals(x.GetArgument<Guid>(0));
                        }
                    )
                );
            });
        }

        private MethodConfiguration<IInformationModelReadRepository> BuildConfigForModelRepo(Guid[] KnownModels)
        {

            return new MethodConfiguration<IInformationModelReadRepository>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Excists(A<Guid>.Ignored)).ReturnsLazily(x => Task.Run(async () => {
                    await Task.Delay(10);
                    return KnownModels.Contains(x.GetArgument<Guid>(0));
                }));
            });
        }

        [Theory]
        // [Create] Happy flow
        [InlineData(ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.KNOW, ValueTypes.MAX, true)]
        [InlineData(ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.MAX, ValueTypes.MAX, true)]
        [InlineData(ValueTypes.MAX, ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.MAX, true)]
        [InlineData(ValueTypes.MAX, ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.KNOW, true)]
        [InlineData(ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.MAX, ValueTypes.KNOW, true)]
        [InlineData(ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.KNOW, ValueTypes.KNOW, false)]
        [InlineData(ValueTypes.KNOW, ValueTypes.KNOW, ValueTypes.KNOW, ValueTypes.KNOW, false)]
        [InlineData(ValueTypes.UNKNOW, ValueTypes.KNOW, ValueTypes.KNOW, ValueTypes.KNOW, false)]
        public void Create_WithValidInput_ReturnsCreatedMapping(ValueTypes id, ValueTypes modelFrom, ValueTypes modelTo, ValueTypes name, bool maxAreKnow)
        {
            model = new TestModel(maxAreKnow);
            //this.BuildAUutInstance(new Guid[] { ModelToRef, ModelFromRef })
            this.BuildAUutInstance()
                .SoThatFunction("Create")
                //.WithParams(new object[] { new MappingModel { ID = new Guid(), modelFrom = model.ModelLeftID, modelTo = model.ModelRightID, Name = "test" } })
                .WithParams(new object[] {
                    new MappingModel {
                        ID = TestModel.ValueManager.GetValue(id, Guid.NewGuid()),
                        modelFrom = TestModel.ValueManager.GetValue(modelFrom, model.ModelLeftID),
                        modelTo = TestModel.ValueManager.GetValue(modelTo, model.ModelRightID),
                        Name = TestModel.ValueManager.GetValue(name, "test name"),
                    }
                })
                .ReturnsAsync(new MappingModel { ID = model.mappingID, modelFrom = model.ModelLeftID, modelTo = model.ModelRightID, Name = "test" });
        }

        [Fact]
        // [Create] Unknown model
        public void Create_WithUknownModelFromRef_ThrowsArgumentException()
        {
            //this.BuildAUutInstance(new Guid[] { ModelToRef })
            this.BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingModel { ID = new Guid(), modelFrom = ModelFromRef, modelTo = ModelToRef, Name = "test" } })
                .ThrowsAsync<NotFoundException<Model>, MappingModel>();
        }

        [Fact]
        // [Create] Unknown model
        public void Create_WithUknownModelToRef_ThrowsArgumentException()
        {
            //this.BuildAUutInstance(new Guid[] { ModelFromRef })
            this.BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingModel { ID = new Guid(), modelFrom = ModelFromRef, modelTo = ModelToRef, Name = "test" } })
                .ThrowsAsync<NotFoundException<Model>, MappingModel>();
        }

        [Fact]
        // [Create] Unknown model
        public void Create_WithUknownModelRefs_ThrowsArgumentException()
        {
            //this.BuildAUutInstance(new Guid[] { })
            this.BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { new MappingModel { ID = new Guid(), modelFrom = ModelFromRef, modelTo = ModelToRef, Name = "test" } })
                .ThrowsAsync<NotFoundException<Model>, MappingModel>();
        }

        [Theory]
        // [Delete] Unknown mapping
        [InlineData(ValueTypes.UNKNOW,
            new[] { "mapping is not found" },
            new[] { typeof(NotFoundException<MappingModel>) }
            )]
        [InlineData(ValueTypes.NULL,
            new[] { "Mapping reference is null" },
            new[] { typeof(ArgumentException) }
            )]
        [InlineData(ValueTypes.MAX,
            new[] { "mapping is not found" },
            new[] { typeof(NotFoundException<MappingModel>) }
            )]
        public void Remove_WithInvalidMappingRef_ThrowsArgumentException(ValueTypes type, string[] withOneOfTheseExceptionMessages, Type[] exceptionTypes)
        {
            this.BuildAUutInstance()
                .SoThatFunction("Remove")
                .WithParams(new object[] { TestModel.ValueManager.GetValue<Guid>(type) } )
                .ThrowsAsync<MappingModel>(withOneOfTheseExceptionMessages, exceptionTypes);
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
                .WithParams(new object[] { TestModel.ValueManager.GetValue(type, model.mappingID) })
                .ReturnsAsync(new MappingModel
                {
                    ID = TestModel.ValueManager.GetValue(type, model.mappingID),
                    modelFrom = TestModel.ValueManager.GetValue(type, model.ModelLeftID),
                    modelTo = model.ModelRightID,
                    Name = "TestMapping"
                });
        }


        //"Mapping reference can not be null"
        // [Create] Unknown model
        [Theory]
        [InlineData(
            ValueTypes.MAX, ValueTypes.MAX, ValueTypes.MAX,  ValueTypes.MAX,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model to and from can not be the same model" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.KNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.UNKNOW, ValueTypes.KNOW, ValueTypes.MAX, ValueTypes.KNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.KNOW, ValueTypes.MAX,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model to and from can not be the same model" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.MAX, ValueTypes.MAX, ValueTypes.KNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model to and from can not be the same model" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.MAX, ValueTypes.MAX,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.KNOW, ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.MAX,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.UNKNOW, ValueTypes.UNKNOW, ValueTypes.UNKNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model to and from can not be the same model" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.NULL, ValueTypes.KNOW, ValueTypes.UNKNOW, ValueTypes.UNKNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.UNKNOW, ValueTypes.KNOW, ValueTypes.UNKNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.UNKNOW, ValueTypes.UNKNOW, ValueTypes.KNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model to and from can not be the same model" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.UNKNOW, ValueTypes.KNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.NULL, ValueTypes.UNKNOW, ValueTypes.KNOW, ValueTypes.KNOW,
            new[] { "Name can not be longer then 100 characters", "model is not found" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.KNOW, ValueTypes.KNOW, ValueTypes.UNKNOW,
            new[] { "model to and from can not be the same model", "Name can not be null or empty" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.NULL, ValueTypes.UNKNOW, ValueTypes.UNKNOW, ValueTypes.MAX,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model to and from can not be the same model" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.NULL, ValueTypes.NULL, ValueTypes.NULL,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model to and from can not be the same model", "model reference can not be null" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.NULL, ValueTypes.KNOW, ValueTypes.NULL, ValueTypes.NULL,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model reference can not be null" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        [InlineData(
            ValueTypes.MAX, ValueTypes.NULL, ValueTypes.KNOW, ValueTypes.NULL,
            new[] { "Name can not be longer then 100 characters", "model is not found", "model reference can not be null" },
            new[] { typeof(ArgumentException), typeof(NotFoundException<Model>) }
            )]
        public void Create_WithValidInput_ReturnsCreatedObject(ValueTypes id, ValueTypes modelFrom, ValueTypes modelTo, ValueTypes name, string[] withOneOfTheseExceptionMessages, Type[] exceptionTypes)
        {
            var right = model.ModelRightID;
            if (modelFrom.Equals(modelTo)) right = model.ModelLeftID;
            this.BuildAUutInstance()
                .SoThatFunction("Create")
                .WithParams(new object[] { 
                    new MappingModel { 
                        ID = TestModel.ValueManager.GetValue(id, Guid.NewGuid()), 
                        modelFrom = TestModel.ValueManager.GetValue(modelFrom, model.ModelLeftID),
                        modelTo = TestModel.ValueManager.GetValue(modelTo, right),
                        Name = TestModel.ValueManager.GetValue(name, "test name") + '*',
                    }
                })
                .ThrowsAsync<MappingModel>(withOneOfTheseExceptionMessages, exceptionTypes);
        }
    }
}
