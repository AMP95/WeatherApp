using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace weatherTypeLib
{
    public class Weather
    {
        public WeatherType WeatherType { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public int WindSpeed { get; set; }
        public WindDirrection WindDirrection { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
