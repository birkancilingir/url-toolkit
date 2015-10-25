using System;

namespace UrlToolkit.DataService
{
    public class BaseFilter { }

    public class LongUrlFilter : BaseFilter
    {
        public String shortenedUrl { get; set; }
    }
}
