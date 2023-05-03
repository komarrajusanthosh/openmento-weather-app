using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppForecast.Services.Interface;

namespace WeatherAppForecast.Services.Implementation
{
    public class WeatherService : IWeatherService
    {
         public async Task<RestResponse> GetWeatherInfo(decimal latitude, decimal longitude, int noOfDays, string timeZone)
        {
            // To do: move API URL to config section
            var options = new RestClientOptions("https://api.open-meteo.com/v1/")
            {
                ThrowOnAnyError = true
            };

            var client = new RestClient(options);

            string startDate = DateTime.Now.ToString("yyyy-MM-dd");
            string endDate = DateTime.Now.AddDays(noOfDays).ToString("yyyy-MM-dd");

            RestRequest request = new("https://api.open-meteo.com/v1/forecast?latitude=" + latitude + "&longitude=" + longitude + "" +
               "&daily=temperature_2m_max,temperature_2m_min,snowfall_sum&start_date=" + startDate + "&end_date=" + endDate + "&" +
                "timezone=Australia%2FSydney", Method.Get);

            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            var response = await client.ExecuteGetAsync(request);

            return response;
        }
    }
}
