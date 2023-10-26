using ApplicationCore.Entities.CityAndDistrictEntities;
using ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
    public class CityService : ICityService
    {
        private readonly HttpClient _httpClient;

        public CityService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TurkeyData> GetAllAsync()
        {
            var result = await _httpClient.GetAsync("https://turkiyeapi.herokuapp.com/api/v1/provinces");
            TurkeyData? data = await result.Content.ReadFromJsonAsync<TurkeyData>();
            
            return data!;
        }

        public async Task<City> GetCityAsync(int cityId)
        {
            var result = await _httpClient.GetAsync($"https://turkiyeapi.herokuapp.com/api/v1/provinces/{cityId}");

            CityData? city = await result.Content.ReadFromJsonAsync<CityData>();

            return city.Data!;
        }
    }
}
