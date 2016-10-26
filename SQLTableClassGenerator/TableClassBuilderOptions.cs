namespace SQLTableClassGenerator
{
    public class TableClassBuilderOptions
    {
        public TableClassBuilderOptions(bool generateCtor)
        {
            GenerateConstructor = generateCtor;
        }

        public TableClassBuilderOptions(bool generateCtor, bool isSealed)
            : this(generateCtor)
        {
            IsSealed = isSealed;
        }

        public bool GenerateConstructor { get; }

        public bool IsSealed { get; }
    }
}
