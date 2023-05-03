using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppForecast.Queries;

namespace WeatherAppForecast.Services.Interface
{
    public interface IMediatorService
    {
        Task<RestResponse> GetWeatherForeCastInfo(GetWeatherForecastQuery query);
    }
}
