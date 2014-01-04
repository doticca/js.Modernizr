using Orchard.UI.Resources;

namespace js.Modernizr {
    public class ResourceManifest : IResourceManifestProvider {
        public void BuildManifests(ResourceManifestBuilder builder) {
            var manifest = builder.Add();

            // defaults at full modernizr
            manifest.DefineScript("Modernizr")
                .SetUrl("modernizr.2.7.1.min.js")                
                .SetVersion("2.7.1");
        }
    }
}
