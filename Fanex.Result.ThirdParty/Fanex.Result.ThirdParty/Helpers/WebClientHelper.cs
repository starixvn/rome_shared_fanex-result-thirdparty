namespace Fanex.Result.ThirdParty.Helpers
{
    using System.Net;

    public static class WebClientHelper
    {
        public static string UploadString(string address, string data)
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
