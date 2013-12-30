using Orchard.Data.Migration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Modernizr
{
    public class ModernizrMigrations : DataMigrationImpl {    
        public int Create()
        {
            SchemaBuilder.CreateTable(
                "ModernizrSettingsPartRecord",
                table => table
                             .ContentPartRecord()
                             .Column<string>("ModernizrUrl")
                );
            return 1;
        }
    }
}