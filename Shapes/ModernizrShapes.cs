using System.Linq;
using Orchard;
using Orchard.DisplayManagement.Descriptors;
using Orchard.Environment.Extensions;
using Orchard.UI.Resources;
using js.Modernizr.Services;


namespace js.Modernizr.Shapes
{
    public class ModernizrShapes : IShapeTableProvider
    {
        private readonly IWorkContextAccessor _wca;
        private readonly IModernizrService _modernizrService;
        public ModernizrShapes(IWorkContextAccessor wca, IModernizrService modernizrService)
        {
            _wca = wca;
            _modernizrService = modernizrService;
        }

        public void Discover(ShapeTableBuilder builder)
        {
            builder.Describe("HeadScripts")
                .OnDisplaying(shapeDisplayingContext =>
                {
                    var resourceManager = _wca.GetContext().Resolve<IResourceManager>();
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