using MediatR;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppForecast.Queries;
using WeatherAppForecast.Services.Interface;

namespace WeatherAppForecast.QueryHandlers
{
    public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, RestResponse>
    {
        private readonly IWeatherService _weatherService;

        public GetWeatherForecastQueryHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        async Task<RestResponse> IRequestHandler<GetWeatherForecastQuery, RestResponse>.Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
        {
            var response = await _weatherService.GetWeatherInfo(request.Latitude, request.Longitude, request.NoOfDays, request.TimeZone);

            return response;
        }
    }
}
