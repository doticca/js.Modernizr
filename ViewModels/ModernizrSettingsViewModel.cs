using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Modernizr.ViewModels
{
    public class ModernizrSettingsViewModel
    {
        public string ModernizrUrl { get; set; }
        public bool AutoEnable { get; set; }
        public bool AutoEnableAdmin { get; set; }
        public IEnumerable<string> ModernizrUrlSuggestions { get; set; }
    }
}