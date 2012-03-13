using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Web;
using System.Net;
using System.Collections.Specialized;

namespace DataAvail.DataService
{
    public static class AllowAccessPolicy
    {
        public static bool CheckPolicy(string AccessAllowOrigin, bool AccessAllowCredentials, string Origin)
        {
            return (AccessAllowOrigin == "*" || AccessAllowOrigin == Origin) &&
                   (Origin != "true" || AccessAllowCredentials);

        }

        public static bool FullfillPolicy(string AccessAllowOrigin, bool AccessAllowCredentials, string Origin, NameValueCollection ResponsetHeaders)
        {
            if (!CheckPolicy(AccessAllowOrigin, AccessAllowCredentials, Origin)) return false;

            if (AccessAllowCredentials)
            {
                ResponsetHeaders.Add("Access-Control-Allow-Origin", Origin);
                ResponsetHeaders.Add("Access-Control-Allow-Credentials", "true");
            }
            else
            {
                ResponsetHeaders.Add("Access-Control-Allow-Origin", AccessAllowOrigin);
            }

            return true;
        }
    }
}
