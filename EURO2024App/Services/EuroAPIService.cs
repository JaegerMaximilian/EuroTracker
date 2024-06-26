﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;
using System.Reflection;
using System.Net.Http.Json;
using System.Text.Json.Serialization;



namespace EURO2024App.Services
{
    public class EuroAPIService
    {
        private HttpClient _httpClient;
        public string BaseApiAdress {  get; set; }
        public EuroAPIService()
        {
            //string exePath = Assembly.GetExecutingAssembly().Location;
            //string exeDir = Path.GetDirectoryName(exePath);

            //string projectDir = Directory.GetParent(exeDir).FullName;

            //projectDir = GetCurrentWorkingDirectory();

            //string fullPath = Path.Combine(projectDir, "port.txt");
            //string port = File.ReadAllText(fullPath);

             BaseApiAdress = $"https://localhost:7094/api";
            _httpClient = new HttpClient();
        }

        

        public async Task<List<Spiel>> GetSpiele()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BaseApiAdress + "/spiele/list"),
                Headers =
                            {
                                { "accept", "text/plain" },
                            },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<Spiel> spiele = JsonConvert.DeserializeObject<List<Spiel>>(body);
                return spiele;
            }
            
        }

        public async Task<Spiel> GetSpiel(int spielId)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BaseApiAdress + $"/spiele/{spielId}"),
                Headers =
                            {
                                { "accept", "text/plain" },
                            },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Spiel spiel = JsonConvert.DeserializeObject<Spiel>(body);
                return spiel;
            }
        }

        public async Task<List<Gruppe>> GetGruppen()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BaseApiAdress + "/gruppen/list"),
                Headers =
                            {
                                { "accept", "text/plain" },
                            },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<Gruppe> gruppen = JsonConvert.DeserializeObject<List<Gruppe>>(body);
                return gruppen;
            }

        }

        public async Task<List<EreignisTyp>> GetEreignisTypen()
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(BaseApiAdress + "/ereignistypen/list"),
                Headers =
                            {
                                { "accept", "text/plain" },
                            },
            };
            using (var response = await _httpClient.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                List<EreignisTyp> ereignisTypen = JsonConvert.DeserializeObject<List<EreignisTyp>>(body);
                return ereignisTypen;
            }

        }

        public async Task CreateEvent(Ereignis ereignis)
        {
            if(ereignis.TorNationId == 0)
            {
                ereignis.TorNationId = null;
            }
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync(BaseApiAdress + "/ereignisse/create", ereignis);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
        }


    }
}
