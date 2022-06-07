using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt
{
    class WeatherForecast
    {
        public class temp
        {
            public double day { get; set; }
            public double night { get; set; }
        }

        public class weather
        {
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }

        public class daily
        {
            public long dt { get; set; }
            public temp temp { get; set; }
            public List<weather> weather { get; set; }
            public double uvi { get; set; }
            public double moon_phase { get; set; }
        }

        public class ForecastInfo
        {
            public List<daily> daily { get; set; }
        }
    }
}
