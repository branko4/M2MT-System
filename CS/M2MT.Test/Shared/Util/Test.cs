using System.Reflection;

/// <summary>
/// The classes inside <c>M2MT.Test.Shared.Util</c> are an attempt to improve the way testing.
/// The goal was a) to write a more semantic test definition and b) to write generic tests, where only the behavior is changed.
/// </summary>
namespace M2MT.Test.Shared.Util
{

    public class ConstructorParameterBuilder<ParameterType, UUT>
    {
        private UUTBuilder<UUT> _uutBuilder;

        public ConstructorParameterBuilder(UUTBuilder<UUT> uutBuilder)
        {
            _uutBuilder = uutBuilder;
        }

        // remains for backwards compatibility and might be needed later on
        public UUTBuilder<UUT> Is(Func<ParameterType> value)
        {
            if (value == null) throw new ArgumentNullException($"Function Is expects an argument that is a function, that gives back ${typeof(ParameterType)}");
            ParameterType createdParameter = value.Invoke();
            _uutBuilder.AddBuildedParameter(createdParameter);
            return _uutBuilder;
        }
    }

    public class MethodConfiguration<Owner>
    {
        public readonly Func<Owner, object> _confFunction;

        public MethodConfiguration(Func<Owner, object> confFunction)
        {
            _confFunction = confFunction;
        }
    }

    public class FunctionTestBuilder<UUT>
    {
        private UUT _uut;
        private string _functionName;
        private object[] _params = new object[0];

        public FunctionTestBuilder(UUT uut, string functionName)
        {
            _uut = uut;
            _functionName = functionName;
        }

        public async void ReturnsAsync<EXPECTED_TYPE>(EXPECTED_TYPE EXPECTED)
        {
            Type uutType = typeof(UUT);
            MethodInfo uutMethod = uutType.GetMethod(_functionName);
            Task<EXPECTED_TYPE> asyncedReturn = (Task<EXPECTED_TYPE>)uutMethod.Invoke(this._uut, _params);
            EXPECTED_TYPE actual = await asyncedReturn;

            Assert.Equal(EXPECTED, actual);
        }

        public FunctionTestBuilder<UUT> WithParams(object[] objects)
        {
            _params = objects;
            return this;
        }
    }

    public class UUTBuilder<UUT>
    {
        IList<object> _params = new List<object>();

        // remains for backwards compatibility and might be needed later on
        public ConstructorParameterBuilder<parameter, UUT> WhereConstructorParameter<parameter>()
        {
            return new ConstructorParameterBuilder<parameter, UUT>(this);
        }

        public FunctionTestBuilder<UUT> SoThatFunction(string functionName)
        {
            return new FunctionTestBuilder<UUT>(BuildUUT(), functionName);
        }

        public ParameterBuilder<ParameterType, UUT> AddBuildedParameterAs<ParameterType>(ParameterType parameter)
        {
            return new ParameterBuilder<ParameterType, UUT>(parameter, this);
        }

        public void AddBuildedParameter(object param)
        {
            this._params.Add(param);
        }

        public UUT BuildUUT()
        {
            Type uutType = typeof(UUT);
            return (UUT)Activator.CreateInstance(typeof(UUT), this._params.ToArray());
        }

        public void IsInstanceOf<SuperType>()
        {
            // Arrange
            var UUT = this.BuildUUT();

            // Act
            var actual = (UUT is SuperType);

            // Assert
            Assert.True(actual);
        }
    }

    public class TestA
    {
        public static UUTBuilder<uut> ObjectWithType<uut>()
        {
            return new UUTBuilder<uut>();
        }
    }
}
