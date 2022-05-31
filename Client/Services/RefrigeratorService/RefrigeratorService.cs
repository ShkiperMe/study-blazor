using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;

using Fridge.Shared;


namespace Fridge.Client.Services.RefrigeratorService
{
    public class RefrigeratorService : IRefrigeratorService
    {
        private readonly HttpClient _http;

        public RefrigeratorService(HttpClient http)
        {
            _http = http;
        }

        public List<Refrigerator> Refrigerators { get; set; } = new List<Refrigerator>();

        public List<Indicator> Indicators { get; set; } = new List<Indicator>();

        public async Task GetRefrigerators()
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Refrigerator>>>("api/refrigerator");
            Refrigerators = result.Data;
        }

        public async Task GetIndicators(int refrigeratorId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<Indicator>>>($"api/indicator/{refrigeratorId}");
            Indicators = result.Data;
        }

        public async Task<Refrigerator> CreateRefrigerator(Refrigerator refrigerator)
        {
            var result = await _http.PostAsJsonAsync("api/refrigerator", refrigerator);
            var newRefrigerator = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Refrigerator>>()).Data;
            return newRefrigerator;
        }

        public async Task<Indicator> CreateRefrigeratorIndicator(Indicator indicator)
        {
            var result = await _http.PostAsJsonAsync("api/indicator", indicator);
            var newIndicator = (await result.Content
                .ReadFromJsonAsync<ServiceResponse<Indicator>>()).Data;
            return newIndicator;
        }

        public async Task DeleteRefrigerator(Refrigerator refrigerator)
        {
            var result = await _http.DeleteAsync($"api/refrigerator/{refrigerator.Id}");
        }

        public async Task<ServiceResponse<Refrigerator>> GetRefrigerator(int refrigeratorId)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<Refrigerator>>($"api/refrigerator/{refrigeratorId}");
            return result;
        }

        public async Task<Refrigerator> UpdateRefrigerator(Refrigerator refrigerator)
        {
            var result = await _http.PutAsJsonAsync($"api/refrigerator", refrigerator);
            var content = await result.Content.ReadFromJsonAsync<ServiceResponse<Refrigerator>>();
            return content.Data;
        }
    }
}
