using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Resources;

namespace UrlToolkit.Common
{
    public class ProjectUtilFunctions
    {
        public static String getUserAgent()
        {            
            PackageVersion version = Package.Current.Id.Version;
            String appVersion = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            ResourceLoader resourceLoader = new ResourceLoader();

            String userAgent = resourceLoader.GetString("ApplicationName") + "/" + appVersion + " (" + ProjectConstants.FEEDBACK_MAIL_ADDRESS + ")";
            // URL Toolkit/1.0.0.0 (urltoolkitapp@outlook.com)

            return userAgent;
        }
    }
}
