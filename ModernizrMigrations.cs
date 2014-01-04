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
        public int UpdateFrom1()
        {
            SchemaBuilder.AlterTable("ModernizrSettingsPartRecord", table => table
                .AddColumn<bool>("AutoEnable", c => c.WithDefault(true)));
            SchemaBuilder.AlterTable("ModernizrSettingsPartRecord", table => table
                .AddColumn<bool>("AutoEnableAdmin", c => c.WithDefault(true)));
            return 2;
        }
    }
}