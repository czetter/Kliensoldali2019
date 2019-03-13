using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Kliensoldali2019_CP6OG3.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Kliensoldali2019_CP6OG3.Services
{
    class Requester
    {
        private String appID = "46956038";
        private String appKey = "bb515d22c13334820a254ca4484e8add";

        //Use try-catch
        public async Task<JsonResult> requestJsonResult(String uri)
        {
            return await GetAsync<JsonResult>(uri);

        }

        //Use try-catch
        public async Task<JsonLanguage> requestJsonLanguage(String uri)
        {
            return await GetAsync<JsonLanguage>(uri);
        }
        
        private async Task<T> GetAsync<T>(String uri)
        {
            HttpWebRequest req = null;
            req = (HttpWebRequest)HttpWebRequest.Create(uri);
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("app_id", appID);
            client.DefaultRequestHeaders.Add("app_key", appKey);
            var response = await client.GetAsync(uri);
            var json = await response.Content.ReadAsStringAsync();
            ManageResponse(response.StatusCode);
            T result = JsonConvert.DeserializeObject<T>(json);
            return result;

        }

        private void ManageResponse(HttpStatusCode statusCode)
        {
            Debug.WriteLine("Requester.cs/ManageResponse " + statusCode + ", " + (int)statusCode);
            int statusNum = (int)statusCode;
            switch (statusNum)
            {
                case 200:
                    return;
                case 400:
                    return;
                case 404:
                    throw new Exception("Nincs találat :(");
                case 500:
                    throw new Exception("Internal Error");
                default:
                    return;
            }
        }

    }
}
