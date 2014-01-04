using Orchard.ContentManagement.Records;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Modernizr.Models
{
    public class ModernizrSettingsPartRecord: ContentPartRecord
    {
        public virtual string ModernizrUrl { get; set; }
        public virtual bool AutoEnable { get; set; }
        public virtual bool AutoEnableAdmin { get; set; }

        public ModernizrSettingsPartRecord()
        {
            ModernizrUrl = string.Empty;
            AutoEnable = true;
            AutoEnableAdmin = true;
        }
    }
}