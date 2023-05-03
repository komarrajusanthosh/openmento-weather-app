using MediatR;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppForecast.Queries;
using WeatherAppForecast.Services.Interface;

namespace WeatherAppForecast.Services.Implementation
{
    public class MediatorService : IMediatorService
    {
        private readonly IMediator _mediator;

        public MediatorService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<RestResponse> GetWeatherForeCastInfo(GetWeatherForecastQuery query)
        {
            return await _mediator.Send(query);
        }
    }
}

