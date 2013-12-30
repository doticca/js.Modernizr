using System;
using System.Collections.Generic;
using System.Linq;
using Orchard;
using Orchard.Caching;
using Orchard.Environment.Extensions;
using Orchard.MediaLibrary.Services;
using js.Modernizr.Models;

namespace js.Modernizr.Services
{
    public interface IModernizrService : IDependency
    {
        string GetModernizrUrl();
        IEnumerable<string> GetModernizrSuggestions();
    }

    public class ModernizrService : IModernizrService
    {
        private readonly IWorkContextAccessor _wca;
        private readonly ICacheManager _cacheManager;
        private readonly ISignals _signals;
        private readonly IMediaLibraryService _mediaService;

        private const string ScriptsFolder = "scripts";

        public ModernizrService(IWorkContextAccessor wca, ICacheManager cacheManager, ISignals signals, IMediaLibraryService mediaService)
        {
            _wca = wca;
            _cacheManager = cacheManager;
            _signals = signals;
            _mediaService = mediaService;
        }

        public string GetModernizrUrl()
        {
            return _cacheManager.Get(
                "js.Modernizr.Url",
                ctx =>
                {
                    ctx.Monitor(_signals.When("js.Modernizr.Changed"));
                    WorkContext workContext = _wca.GetContext();
                    var modernizrSettings =
                        (ModernizrSettingsPart)workContext
                                                  .CurrentSite
                                                  .ContentItem
                                                  .Get(typeof(ModernizrSettingsPart));
                    return modernizrSettings.ModernizrUrl;
                });
        }

        public IEnumerable<string> GetModernizrSuggestions()
        {
            List<string> modernizrSuggestions = null;
            var rootMediaFolders = _mediaService
                .GetMediaFolders(".")
                .Where(f => f.Name.Equals(ScriptsFolder, StringComparison.OrdinalIgnoreCase));
            if (rootMediaFolders.Any())
            {
                modernizrSuggestions = new List<string>(
                    _mediaService.GetMediaFiles(ScriptsFolder)
                        .Select(f => _mediaService.GetMediaPublicUrl(ScriptsFolder, f.Name))
                        .Where(j => j.ToLower().IndexOf("modernizr")>0 && j.ToLower().EndsWith(".js"))
                        );
            }
            return modernizrSuggestions;
        }

    }
}