namespace Fanex.Result.ThirdParty.Helpers
{
    using System;
    using System.Net;

    public static class WebClientHelper
    {
        [ThreadStatic]
        private static Func<string, string, string> uploadStringFunc;

        public static Func<string, string, string> CallHttpPost
        {
            get
            {
                return uploadStringFunc ?? UploadString;
            }
            internal set
            {
                uploadStringFunc = value;
            }
        }

        internal static string UploadString(string address, string data)
        {
            var responseString = string.Empty;

            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                responseString = webClient.UploadString(address, data);
            }

            return responseString;
        }
    }
}
