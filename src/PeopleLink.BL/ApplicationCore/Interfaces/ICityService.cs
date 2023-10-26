using ApplicationCore.Entities.CityAndDistrictEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ICityService
    {
        Task<TurkeyData> GetAllAsync();

        Task<City> GetCityAsync(int cityId);
    }
}
