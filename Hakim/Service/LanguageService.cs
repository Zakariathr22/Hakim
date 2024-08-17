using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Windows.ApplicationModel.Resources;
using Windows.Globalization;


namespace Hakim.Service
{
    public static class LanguageService
    {
        private static ResourceMap resourceMap;
        private static ResourceManager context;
        private static ResourceContext resourceContext;

        public static void SetLanguage(string language)
        {
            // Set the primary language override
            ApplicationLanguages.PrimaryLanguageOverride = language;

            // Create a ResourceContext and ResourceManager in WinUI 3
            context = new ResourceManager();
            resourceMap = context.MainResourceMap.GetSubtree("Resources");

            // Apply the new language to the context
            resourceContext = context.CreateResourceContext();
            resourceContext.QualifierValues["Language"] = language;
        }

        public static string GetResourceValue(string name)
        {
            return resourceMap.GetValue(name, resourceContext).ValueAsString;
        }
    }
}
