using System.Text;
using DinkToPdf.Contracts;
using DinkToPdf.Utils;

namespace DinkToPdf.Settings
{
    public class ObjectSettings : IObject
    {
        /// <summary>
        /// The URL or path of the web page to convert, if "-" input is read from stdin. Default = ""
        /// </summary>
        [WkHtml("page")]
        public string Page { get; set; }

        /// <summary>
        /// Should external links in the HTML document be converted into external pdf links. Default = true
        /// </summary>
        [WkHtml("useExternalLinks")]
        public bool? UseExternalLinks { get; set; }

        /// <summary>
        /// Should internal links in the HTML document be converted into pdf references. Default = true
        /// </summary>
        [WkHtml("useLocalLinks")]
        public bool? UseLocalLinks { get; set; }

        /// <summary>
        /// Should we turn HTML forms into PDF forms. Default = false
        /// </summary>
        [WkHtml("produceForms")]
        public bool? ProduceForms { get; set; }

        /// <summary>
        /// Should the sections from this document be included in the outline and table of content. Default = false
        /// </summary>
        [WkHtml("includeInOutline")]
        public bool? IncludeInOutline { get; set; }

        /// <summary>
        /// Should we count the pages of this document, in the counter used for TOC, headers and footers. Default = false
        /// </summary>
        [WkHtml("pagesCount")]
        public bool? PagesCount { get; set; }

        public string HtmlContent { get; set; }

        private WebSettings _webSettings = new WebSettings();

        public WebSettings WebSettings {
            get { return _webSettings; }
            set { _webSettings = value; }
        }

        private HeaderSettings _headerSettings = new HeaderSettings();

        public HeaderSettings HeaderSettings {
            get { return _headerSettings; }
            set { _headerSettings = value; }
        }

        private FooterSettings _footerSettings = new FooterSettings();

        public FooterSettings FooterSettings {
            get { return _footerSettings; }
            set { _footerSettings = value; }
        }

        private LoadSettings _loadSettings = new LoadSettings();

        public LoadSettings LoadSettings {
            get { return _loadSettings; }
            set { _loadSettings = value; }
        }

        public byte[] GetContent()
        {
            if (HtmlContent == null)
            {
                return new byte[0];
            }

            return Encoding.UTF8.GetBytes(HtmlContent);
        }
    }
}
