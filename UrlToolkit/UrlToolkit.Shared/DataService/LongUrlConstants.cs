using System;

namespace UrlToolkit.DataService
{
    public class LongUrlConstants
    {
        public const String API_ENDPOINT = "http://api.longurl.org/v2";

        public enum ErrorResponses 
        { 
            OK = 200, 
            BAD_REQUEST = 400, 
            NOT_FOUND = 404, 
            INTERNAL_SERVER_ERROR = 500, 
            SERVICE_UNAVAILABLE = 503, 
            BAD_GATEWAY = 504 
        }
    }
}
