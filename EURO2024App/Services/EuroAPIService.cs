using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace EURO2024App.Services
{
    public class EuroAPIService
    {
        private HttpClient _httpClient;
        //public string BaseApiAdress {  get; set; }
        public EuroAPIService()
        {
           // BaseApiAdress = baseApiAdress;
            _httpClient = new HttpClient();
        }

        List<Spiel> gameList;

        public async Task<List<Spiel>> GetSpiele()
        {
            //// Offline
            //using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            //using var reader = new StreamReader(stream);
            //var contents = await reader.ReadToEndAsync();
            //gameList = System.Text.Json.JsonSerializer.Deserialize(contents, System.Text.Json.SpielContext.Default.ListSpiel);

            //return gameList;

            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7094/api/spiele/List");

            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:127.0) Gecko/20100101 Firefox/127.0");
            request.Headers.Add("Accept", "text/plain");
            request.Headers.Add("Accept-Language", "de,en-US;q=0.7,en;q=0.3");
            // request.Headers.Add("Accept-Encoding", "gzip, deflate, br, zstd");
            request.Headers.Add("Referer", "https://localhost:7094/swagger/index.html");
            request.Headers.Add("DNT", "1");
            request.Headers.Add("Connection", "keep-alive");
            request.Headers.Add("Sec-Fetch-Dest", "empty");
            request.Headers.Add("Sec-Fetch-Mode", "cors");
            request.Headers.Add("Sec-Fetch-Site", "same-origin");
            request.Headers.Add("Priority", "u=1");
            request.Headers.Add("TE", "trailers");

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string body = await response.Content.ReadAsStringAsync();
            List<Spiel> spiele = JsonConvert.DeserializeObject<List<Spiel>>(body);
            return spiele;
        }
    }
}
