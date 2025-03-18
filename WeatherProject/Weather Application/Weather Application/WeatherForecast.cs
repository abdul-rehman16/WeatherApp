using System;
using System.Collections.Generic;

namespace Weather_Application
{
    class WeatherForecast
    {
        public class Temp
        {
            public double Day { get; set; }
        }

        public class Weather
        {
            public string Main { get; set; }
            public string Icon { get; set; }
            public string Description { get; set; }
        }

        public class Daily
        {
            public long Dt { get; set; }
            public Temp Temp { get; set; }
            public List<Weather> Weather { get; set; }
        }

        public class ForecastInfo
        {
            public List<Daily> Daily { get; set; }
        }
    }
}
