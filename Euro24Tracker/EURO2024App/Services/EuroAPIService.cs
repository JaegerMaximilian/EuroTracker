using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Net.Http.Json;
using Newtonsoft.Json;
using EURO2024App.Types;

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
            // Offline
            using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            
            gameList = JsonSerializer.Deserialize(contents, SpielContext.Default.ListSpiel);

            return gameList;
        }

        //public async Task<List<Spiel>> GetGruppen()
        //{
        //    // Offline
        //    using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        //    using var reader = new StreamReader(stream);
        //    var contents = await reader.ReadToEndAsync();

        //    gameList = JsonSerializer.Deserialize(contents, GruppeContext.Default.ListSpiel);

        //    //List<Spiel> spiele = JsonConvert
        //    return gameList;
        //}
    }
}
