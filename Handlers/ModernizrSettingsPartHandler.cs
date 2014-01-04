using js.Modernizr.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace js.Modernizr.Handlers
{
    public class ModernizrSettingsPartHandler: ContentHandler {
        public ModernizrSettingsPartHandler(IRepository<ModernizrSettingsPartRecord> repository)
        {
            T = NullLocalizer.Instance;
            Filters.Add(StorageFilter.For(repository));
            Filters.Add(new ActivatingFilter<ModernizrSettingsPart>("Site"));

            OnInitializing<ModernizrSettingsPart>((context, part) =>
            {
                part.AutoEnable = true;
                part.AutoEnableAdmin = true;
                part.ModernizrUrl = string.Empty;
            });
        }

        public Localizer T { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context) {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Modernizr")));
        }
    }
}