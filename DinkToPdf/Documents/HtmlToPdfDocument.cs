using System.Collections.Generic;
using DinkToPdf.Contracts;
using DinkToPdf.Settings;

namespace DinkToPdf.Documents
{
    public class HtmlToPdfDocument : IDocument
    {
        public List<ObjectSettings> Objects { get;  private set;}
        
        private GlobalSettings _globalSettings = new GlobalSettings();

        public GlobalSettings GlobalSettings {
            get { return _globalSettings; }
            set {
                _globalSettings = value;
            }
        }

        public HtmlToPdfDocument()
        {
            Objects = new List<ObjectSettings>();
        }

        public IEnumerable<IObject> GetObjects()
        {
            return Objects.ToArray();
        }   
    }
}
