using System;

namespace XtramileTask2.Models
{
    public class WeatherInfo
    {
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public string Wind { get; set; }
        public string Visibility { get; set; }
        public string SkyConditions { get; set; }
        public double TemperatureCelsius { get; set; }
        public double TemperatureFahrenheit { get; set; }
        public double DewPoint { get; set; }
        public string RelativeHumidity { get; set; }
        public string Pressure { get; set; }
        //public double RelativeHumidity { get; set; }
        //public double Pressure { get; set; }
    }
}
