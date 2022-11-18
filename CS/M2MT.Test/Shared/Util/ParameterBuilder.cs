namespace M2MT.Test.Shared.Util
{
    public class ParameterBuilder<ParameterType, UUT>
    {
        private ParameterType? parameter;
        private UUTBuilder<UUT> uUTBuilder;

        public ParameterBuilder(ParameterType? parameter, UUTBuilder<UUT> uUTBuilder)
        {
            this.parameter = parameter;
            this.uUTBuilder = uUTBuilder;
        }

        public UUTBuilder<UUT> WithMethodConfiguration(MethodConfiguration<ParameterType>[] conf)
        {
            foreach (var confItem in conf)
            {
                confItem._confFunction.Invoke(parameter);
            }

            uUTBuilder.AddBuildedParameter(parameter);
            return uUTBuilder;
        }
    }
}