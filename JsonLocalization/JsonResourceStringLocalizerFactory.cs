using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace JsonLocalization
{
    public class JsonResourceStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly string _resourcesRelativePath;
        public JsonResourceStringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions)
        {
            if (localizationOptions == null)
            {
                throw new ArgumentNullException(nameof(localizationOptions));
            }
            _resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? String.Empty;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            if (resourceSource == null)
            {
                throw new ArgumentNullException(nameof(resourceSource));
            }

            var typeInfo = resourceSource.GetTypeInfo();
            var source = typeInfo.FullName.Substring(typeInfo.FullName.IndexOf('.') + 1);
            return new JsonResourceStringLocalizer(_resourcesRelativePath, source);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            var source = baseName.Substring(location.Length + 1);
            return new JsonResourceStringLocalizer(_resourcesRelativePath, source);
        }

        private string GetResourcePath(Assembly assembly)
        {
            var resourceLocationAttribute = assembly.GetCustomAttribute<ResourceLocationAttribute>();

            var resourceLocation = resourceLocationAttribute == null
                                       ? _resourcesRelativePath
                                       : resourceLocationAttribute.ResourceLocation + ".";
            resourceLocation = resourceLocation
                               .Replace(Path.DirectorySeparatorChar, '.')
                               .Replace(Path.AltDirectorySeparatorChar, '.');
            return resourceLocation;
        }
    }
}
