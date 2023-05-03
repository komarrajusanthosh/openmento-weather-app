using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppForecast.Services.Interface
{
    public interface IWeatherService
    {
        public Task<RestResponse> GetWeatherInfo(decimal latitude, decimal longitude, int noOfDays, string timeZone);
    }
}
