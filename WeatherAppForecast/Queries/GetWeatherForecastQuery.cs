using MediatR;
using RestSharp;

namespace WeatherAppForecast.Queries
{
    public class GetWeatherForecastQuery : IRequest<RestResponse>
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int NoOfDays { get; set; }
        public string TimeZone { get; set; }
    }
}
