using System;

namespace ClassGeneration.Models
{
    public class Column
    {
        public Column(string columnName, Type columnType)
        {
            Type = columnType;
            Field = columnName;
        }

        public Type Type
        {
            get;
        }

        public string Field
        {
            get;
        }
    }
}