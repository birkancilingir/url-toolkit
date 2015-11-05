using System;
using System.Collections.Generic;
using System.Text;

namespace UrlToolkit.DataService
{
    public class LongUrlDataServiceException : Exception
    {
        public string errorMessage { get; set; }
     
        public LongUrlDataServiceException(String errorMessage)
        {
            this.errorMessage = errorMessage;
        }
    }
}
