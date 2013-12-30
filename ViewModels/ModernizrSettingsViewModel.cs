using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Modernizr.ViewModels
{
    public class ModernizrSettingsViewModel
    {
        public string ModernizrUrl { get; set; }
        public IEnumerable<string> ModernizrUrlSuggestions { get; set; }
    }
}