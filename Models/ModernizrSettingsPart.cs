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
        public bool AutoEnable
        {
            get { return Record.AutoEnable; }
            set { Record.AutoEnable = value; }
        }
        public bool AutoEnableAdmin
        {
            get { return Record.AutoEnableAdmin; }
            set { Record.AutoEnableAdmin = value; }
        }
    }
}