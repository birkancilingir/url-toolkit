using Newtonsoft.Json.Linq;
using System;

namespace UrlToolkit.DataService.Entities
{
    /// <example> {"long-url":"http:\/\/www.google.com\/"} </example>
    public class LongUrl
    {
        /// <summary> Long Url </summary>
        /// <remarks> Json Reponse Name: owner </remarks>

        public string Url { get; set; }

        /// <summary> Parses an url json string </summary>
        public static LongUrl mapItemToLongUrl(JObject longUrlItem)
        {
            LongUrl parsedLongUrl = new LongUrl();

            parsedLongUrl.Url = longUrlItem["long-url"] == null ? String.Empty : (string)longUrlItem["long-url"];

            return parsedLongUrl;
        }
    }
}
