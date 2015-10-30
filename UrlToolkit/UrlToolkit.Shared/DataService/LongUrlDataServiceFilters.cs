using System;

namespace UrlToolkit.DataService
{
    public class BaseFilter
    {
        public ResponseFormat Format { get; set; }
    }

    public class ServicesFilter : BaseFilter { }

    public class ExpandUrlFilter : BaseFilter
    {
        public String Url { get; set; }
        public Argument AllRedirects { get; set; }
        public Argument ContentType { get; set; }
        public Argument ResponseCode { get; set; }
        public Argument HtmlTitle { get; set; }
        public Argument CanonicalUrl { get; set; }
        public Argument MetaKeywords { get; set; }
        public Argument MetaDescription { get; set; }
    }

    public enum ResponseFormat { XML, JSON, PHP }
    public enum Argument { INCLUDE, EXCLUDE }
}
