using Newtonsoft.Json.Linq;
using System;

namespace UrlToolkit.DataService.Entities
{
    /// <example> {"0rz.tw":{"domain":"0rz.tw","regex":""},"1link.in":{"domain":"1link.in","regex":""} ... </example>
    public class Service
    {
        /// <summary> Name of the domain </summary>
        /// <remarks> Json Reponse Name: name </remarks>
        public string Name { get; set; }

        /// <summary> Domain </summary>
        /// <remarks> Json Reponse Name: domain </remarks>
        public string Domain { get; set; }

        /// <summary> Regex </summary>
        /// <remarks> Json Reponse Name: regex </remarks>
        public string Regex { get; set; }

        /// <summary> Parses a service json string </summary>
        public static Service mapItemToService(JObject serviceItem)
        {
            Service parsedService = new Service();

            parsedService.Name = serviceItem["name"] == null ? String.Empty : (string)serviceItem["name"];
            parsedService.Domain = serviceItem["domain"] == null ? String.Empty : (string)serviceItem["domain"];
            parsedService.Regex = serviceItem["regex"] == null ? String.Empty : (string)serviceItem["regex"];

            return parsedService;
        }
    }
}
