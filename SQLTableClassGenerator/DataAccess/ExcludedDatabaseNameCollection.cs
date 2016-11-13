using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SQLTableClassGenerator.DataAccess
{
    public class ExcludedDatabaseNameCollection : Collection<string>
    {
        public ExcludedDatabaseNameCollection()
            : base(new List<string>
            {
                "master",
                "model",
                "tempdb",
                "msdb"
            })
        {
        }
    }
}
