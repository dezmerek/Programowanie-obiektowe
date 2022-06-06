using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekt
{
    internal class WeatherInfo
    {
        public class weather
        {
            public string main { get; set; }
            public string description { get; set; }
        }

        public class root
        {
            public List <weather> weather { get; set; }
        }
    }
}
