using swapi.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;


namespace swapi.Services
{
    public class SwapiService
    {
        private HttpClient _client;
        public SwapiService() 
        { 
            _client = new HttpClient() {BaseAddress = new Uri("https://swapi.dev/api/")};
        }
        public async Task<Planet> GetPlanet(int id)
        {
            var response = await _client.GetAsync($"planets/{id}/");

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            var planet = new Planet
            {
                Name = data.name,
                Gravity = data.gravity,
                Residents = await GetResidents(data.residents as JArray)
            };

            return planet; 
        }

        private async Task<List<Resident>> GetResidents(JArray residentUrls)
        {
            if (residentUrls == null || !residentUrls.HasValues)
            {
                return new List<Resident>();
            }

            var residentTasks = new List<Task<Resident>>();

            foreach (var url in residentUrls)
            {
                residentTasks.Add(GetResident(url.ToString()));
            }

            return (await Task.WhenAll(residentTasks)).ToList(); 
        }

        private async Task<Resident> GetResident(string url)
        {
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {response.StatusCode}, Reason: {response.ReasonPhrase}");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

            return new Resident { Name = data.name }; 
        }
    }
}
