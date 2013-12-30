using js.Modernizr.Models;
using js.Modernizr.Services;
using Orchard.Caching;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.ContentManagement;
using Orchard.Environment.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using js.Modernizr.ViewModels;

namespace js.Modernizr.Drivers
{
    public class ModernizrSettingsPartDriver : ContentPartDriver<ModernizrSettingsPart> 
    {
        private readonly ISignals _signals;
        private readonly IModernizrService _modernizrService;

        public ModernizrSettingsPartDriver(ISignals signals, IModernizrService modernizrService)
        {
            _signals = signals;
            _modernizrService = modernizrService;
        }

        protected override string Prefix { get { return "ModernizrSettings"; } }

        protected override DriverResult Editor(ModernizrSettingsPart part, dynamic shapeHelper)
        {
            var modernizrSuggestions = _modernizrService.GetModernizrSuggestions();

            return ContentShape("Parts_Modernizr_ModernizrSettings",
                               () => shapeHelper.EditorTemplate(
                                   TemplateName: "Parts/Modernizr.ModernizrSettings",
                                   Model: new ModernizrSettingsViewModel
                                   {
                                       ModernizrUrl = part.ModernizrUrl,
                                       ModernizrUrlSuggestions = modernizrSuggestions
                                   },
                                   Prefix: Prefix)).OnGroup("Modernizr");
        }

        protected override DriverResult Editor(ModernizrSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part.Record, Prefix, null, null);
            _signals.Trigger("js.Modernizr.Changed");
            return Editor(part, shapeHelper);
        }

        protected override void Exporting(ModernizrSettingsPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("ModernizrUrl", part.Record.ModernizrUrl);
        }

        protected override void Importing(ModernizrSettingsPart part, ImportContentContext context)
        {
            part.Record.ModernizrUrl = context.Attribute(part.PartDefinition.Name, "ModernizrUrl");
        }
    }
}