using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppForecast.Helpers
{
    public static class ConversionsHelper
    {
        public static double CelsiusToFahrenheit(double celsius)
        {
            return Math.Round(((celsius * 9) / 5) + 32, 2);
        }
    }
}
