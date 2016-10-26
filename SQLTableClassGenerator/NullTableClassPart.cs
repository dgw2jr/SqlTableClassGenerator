namespace SQLTableClassGenerator
{
    public class NullTableClassPart : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            return string.Empty;
        }
    }
}