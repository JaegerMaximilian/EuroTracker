using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<List<Spiel>> GetGames()
        {
            // Offline
            using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            gameList = JsonSerializer.Deserialize(contents, SpielContext.Default.ListSpiel);

            return gameList;
        }
    }
}
