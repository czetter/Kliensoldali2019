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


        public async Task<JsonResult> requestJsonResult(String uri)
        {
            return await GetAsync<JsonResult>(uri);

        }


        public async Task<JsonLanguage> requestJsonLanguage(String uri)
        {
            return await GetAsync<JsonLanguage>(uri);
        }

        private async Task<T> GetAsync<T>(String uri)
        {
            HttpWebRequest req = null;
            req = (HttpWebRequest)HttpWebRequest.Create(uri);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("app_id", appID);
                client.DefaultRequestHeaders.Add("app_key", appKey);
                var response = await client.GetAsync(uri);
                var json = await response.Content.ReadAsStringAsync();
                T result = JsonConvert.DeserializeObject<T>(json);
                return result;
            }
            
        }
      
    }
}
