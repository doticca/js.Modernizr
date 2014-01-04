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
                                       AutoEnable = part.AutoEnable,
                                       AutoEnableAdmin = part.AutoEnableAdmin,
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
            var element = context.Element(part.PartDefinition.Name);

            element.SetAttributeValue("ModernizrUrl", part.ModernizrUrl);
            element.SetAttributeValue("AutoEnable", part.AutoEnable);
            element.SetAttributeValue("AutoEnableAdmin", part.AutoEnableAdmin);
        }

        protected override void Importing(ModernizrSettingsPart part, ImportContentContext context)
        {
            var partName = part.PartDefinition.Name;

            part.Record.ModernizrUrl = GetAttribute<string>(context, partName, "ModernizrUrl");
            part.Record.AutoEnable = GetAttribute<bool>(context, partName, "AutoEnable");
            part.Record.AutoEnableAdmin = GetAttribute<bool>(context, partName, "AutoEnableAdmin");
        }

        private TV GetAttribute<TV>(ImportContentContext context, string partName, string elementName)
        {
            string value = context.Attribute(partName, elementName);
            if (value != null)
            {
                return (TV)Convert.ChangeType(value, typeof(TV));
            }
            return default(TV);
        }
    }
}