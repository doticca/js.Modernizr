using Orchard.ContentManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Modernizr.Models
{
    public class ModernizrSettingsPart : ContentPart<ModernizrSettingsPartRecord> {    
        public string ModernizrUrl
        {
            get { return Record.ModernizrUrl; }
            set { Record.ModernizrUrl = value; }
        }
    }
}