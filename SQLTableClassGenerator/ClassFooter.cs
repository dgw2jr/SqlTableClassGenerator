namespace SQLTableClassGenerator
{
    public class ClassFooter : ITableClassPart
    {
        public string GetString(TableDef table)
        {
            return "}";
        }
    }
}