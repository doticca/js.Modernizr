using System.Linq;
using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;
using js.Modernizr.Services;
using Orchard.Environment;
using Orchard.UI.Admin;


namespace js.Modernizr.Shapes
{
    public class ModernizrShapes : IShapeTableProvider
    {
        private readonly Work<WorkContext> _workContext;
        private readonly IModernizrService _modernizrService;
        public ModernizrShapes(Work<WorkContext> workContext, IModernizrService modernizrService)
        {
            _workContext = workContext;
            _modernizrService = modernizrService;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("HeadScripts")
                .OnDisplaying(shapeDisplayingContext =>
                {
                    if (!_modernizrService.GetAutoEnable()) return;
                    if (!_modernizrService.GetAutoEnableAdmin())
                    {
                        var request = _workContext.Value.HttpContext.Request;
                        if (AdminFilter.IsApplied(request.RequestContext))
                        {
                            return;
                        }
                    }

                    var resourceManager = _workContext.Value.Resolve<IResourceManager>();
                    var scripts = resourceManager.GetRequiredResources("script");

                    var currentModernizr = scripts
                            .Where(l => l.Name == "Modernizr")
                            .FirstOrDefault();

                    if (currentModernizr == null)
                    {
                        string modernizrUrl = _modernizrService.GetModernizrUrl();
                        if (string.IsNullOrWhiteSpace(modernizrUrl))
                        {
                            resourceManager.Require("script", "Modernizr").AtHead();
                        }
                        else
                        {
                            resourceManager.Include("script", modernizrUrl, modernizrUrl).AtHead();
                        }
                    }                    
                });
        }
    }
}