using MediatR;
using Moq;
using RestSharp;
using WeatherAppForecast.Queries;
using WeatherAppForecast.QueryHandlers;
using WeatherAppForecast.Services.Implementation;
using WeatherAppForecast.Services.Interface;

namespace WeatherApp.Tests
{
    public class WeatherForeCastTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly IMediatorService _mediatorService;
        private readonly IWeatherService _weatherService;
        private IRequestHandler<GetWeatherForecastQuery, RestResponse> _getWeatherForecastQueryHandler;

        public WeatherForeCastTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _weatherService = new WeatherService();
            _mediatorService = new MediatorService(_mediatorMock.Object);
            _getWeatherForecastQueryHandler = new GetWeatherForecastQueryHandler(_weatherService);
        }

        [Fact]
        public async void GetWeatherForeCastInfo_Should_Return_WeatherInfoObject()
        {
            //Arrange
            GetWeatherForecastQuery getWeatherForecastQuery = new GetWeatherForecastQuery()
            {
                Latitude = 53.12m,
                Longitude = 13.12m,
                NoOfDays = 2
            };

            var expectedResult = new RestResponse();
            _mediatorMock.Setup(x => x.Send(getWeatherForecastQuery, default)).ReturnsAsync(expectedResult);

            // Act
            var result = await _mediatorService.GetWeatherForeCastInfo(getWeatherForecastQuery);

            // Assert
            Assert.True(result.ContentType == expectedResult.ContentType);
        }

        [Fact]
        public async void GetWeatherForeCastInfo_Should_Return_WeatherInfo()
        {
            //Arrange
            GetWeatherForecastQuery getWeatherForecastQuery = new()
            {
                Latitude = 53.12m,
                Longitude = 13.12m,
                NoOfDays = 2
            };

            // Act
            var result = await _getWeatherForecastQueryHandler.Handle(getWeatherForecastQuery, default);

            //Assert
            Assert.True(result.IsSuccessStatusCode);
        }
    }
}