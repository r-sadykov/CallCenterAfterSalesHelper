using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace JsonLocalization
{
    public class JsonResourceStringLocalizer : IStringLocalizer
    {
        List<JsonLocalizationFormat> localization = new List<JsonLocalizationFormat>();
        private readonly string _resourcesRelativePath;
        private readonly string _typeResourceSource;
        public JsonResourceStringLocalizer(string resourcesRelativePath, string resourceSource = "")
        {
            //read all json file
            _resourcesRelativePath = resourcesRelativePath;
            _typeResourceSource = resourceSource;
            if (!String.IsNullOrWhiteSpace(_resourcesRelativePath))
            {
                var individualResourcePath = Path.Combine(Directory.GetCurrentDirectory(), resourcesRelativePath, $"{_typeResourceSource}.json");
                var resourcePath = Path.Combine(Directory.GetCurrentDirectory(), resourcesRelativePath, $"localization.json");
                var dataResourcePath = Path.Combine(Directory.GetCurrentDirectory(), resourcesRelativePath, $"data.json");
                byte[] data;


                #region Use Mozilla Character Detector
                /*
Ude.CharsetDetector detector=new CharsetDetector();
detector.Feed(data,0,data.Length);
detector.DataEnd();
string fileText;
if (detector.Confidence > 0.5f)
{
    fileText= Encoding.GetEncoding(detector.Charset).GetString(data);
}
else
{
    fileText = Encoding.UTF8.GetString(data);
}
*/
                #endregion
                string fileText;
                if (File.Exists(resourcePath))
                {
                    data = File.ReadAllBytes(resourcePath);
                    fileText = Encoding.UTF8.GetString(data);
                    localization.AddRange(JsonConvert.DeserializeObject<List<JsonLocalizationFormat>>(fileText));
                }
                if (File.Exists(individualResourcePath))
                {
                    data = File.ReadAllBytes(individualResourcePath);
                    fileText = Encoding.UTF8.GetString(data);
                    localization.AddRange(JsonConvert.DeserializeObject<List<JsonLocalizationFormat>>(fileText));
                }
                if (File.Exists(dataResourcePath))
                {
                    data = File.ReadAllBytes(dataResourcePath);
                    fileText = Encoding.UTF8.GetString(data);
                    localization.AddRange(JsonConvert.DeserializeObject<List<JsonLocalizationFormat>>(fileText));
                }

                localization = localization.Distinct().ToList();
            }
            else
            {
                localization = JsonConvert.DeserializeObject<List<JsonLocalizationFormat>>(File.ReadAllText(@"localization.json"));
            }
        }
        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }
        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return localization.Where(
                                      l => l.Value.Keys.Any(
                                                            lv => lv == CultureInfo.CurrentCulture.Name))
                               .Select(l => new LocalizedString(
                                                                l.Key, l.Value[CultureInfo.CurrentCulture.Name], true));
        }
        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonResourceStringLocalizer(_resourcesRelativePath);
        }
        private string GetString(string name)
        {
            var i = localization.Count;
            var query = localization.Where(
                                           l => l.Value.Keys.Any(
                                                                 lv => lv == CultureInfo.CurrentCulture.Name));
            var value = query.FirstOrDefault(l => l.Key == name);
            return value == null ? name : value.Value[CultureInfo.CurrentCulture.Name];
        }
    }
}
