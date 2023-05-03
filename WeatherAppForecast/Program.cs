using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Reflection;
using WeatherAppForecast.Helpers;
using WeatherAppForecast.Queries;
using WeatherAppForecast.Services.Implementation;
using WeatherAppForecast.Services.Interface;

using IHost host = Host.CreateDefaultBuilder(args).ConfigureServices(services =>
{
    services.AddScoped<IWeatherService, WeatherService>();
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    services.AddScoped<IMediatorService, MediatorService>();
}).Build();

decimal latitude = 0, longitude = 0;
int noOfDays = 0;

// validations logic start
Console.WriteLine("Enter the latitude, number only:");
while (!decimal.TryParse(Console.ReadLine(), out latitude))
{
    Console.WriteLine("Invalid format, please input again!");
};
Console.Clear();

Console.WriteLine("Enter the longitude, number only:");
while (!decimal.TryParse(Console.ReadLine(), out longitude))
{
    Console.WriteLine("Invalid format, please input again!");
};
Console.Clear();

Console.WriteLine("Enter the noOfDays, number only:");
while (!Int32.TryParse(Console.ReadLine(), out noOfDays))
{
    Console.WriteLine("Invalid format, please input again!");
};
Console.Clear();

// validations logic end

var mediator = host.Services.GetService<IMediatorService>();

// Get Weather ForeCase Info
var response = await mediator.GetWeatherForeCastInfo(new GetWeatherForecastQuery
{
    Latitude = latitude,
    Longitude = longitude,
    NoOfDays = noOfDays,
    TimeZone = "Australia2FSydney" // TimeZone is Manditory added default zone to Australia
});

if (response.IsSuccessStatusCode && response.Content != null)
{
    // Convert the string content to a JSON object
    var jsonObject = JsonConvert.DeserializeObject<dynamic>(response.Content)!;

    // fetch daily object from dynamic json to read temperature info
    var dailyObject = jsonObject.SelectToken("daily");

    // fetch max temparature object
    var maxTeampature = jsonObject.SelectToken("daily").SelectToken("temperature_2m_max");

    // fetch min temparature object
    var minTeampature = jsonObject.SelectToken("daily").SelectToken("temperature_2m_min");

    IEnumerable<object> maxTeampaturecollection = (IEnumerable<object>)maxTeampature;
    List<object> maxTeampatureFahreheut = new();

    foreach (object item in maxTeampaturecollection)
    {
        // Convert temp max - celsius to fahrenheit
        maxTeampatureFahreheut.Add(ConversionsHelper.CelsiusToFahrenheit(Convert.ToDouble(item)));
    }

    dailyObject.Add("temperatureFahrenheit_max", JsonConvert.SerializeObject(maxTeampatureFahreheut));

    IEnumerable<object> minTeampaturecollection = (IEnumerable<object>)minTeampature;
    List<object> minTeampatureFahreheut = new();

    foreach (object item in minTeampaturecollection)
    {
        // Convert temp min - celsius to fahrenheit
        minTeampatureFahreheut.Add(ConversionsHelper.CelsiusToFahrenheit(Convert.ToDouble(item)));
    }

    dailyObject.Add("temperatureFahrenheit_min", JsonConvert.SerializeObject(minTeampatureFahreheut));

    // This will get the current WORKING directory (i.e. \bin\Debug)
    string workingDirectory = Environment.CurrentDirectory;

    // This will get the current PROJECT directory
    string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

    // Combine the directory path with the file name
    // Note : Storing files in project folder will give problume in deployments added only for reference purpose
    // change file location based on deployment type (Cloud, On-prem etc..)
    string filePath = Path.Combine(projectDirectory + "\\Reports", "weatherExport_" + DateTime.Now.ToString("yyyyMMdd") + ".json");

    // Write the JSON object to the file
    File.WriteAllText(filePath, JsonConvert.SerializeObject(jsonObject));

    Console.WriteLine("Weather information file generated in :" + filePath);
}
else
{
    Console.WriteLine("Weather information not generated");
}

await host.RunAsync();