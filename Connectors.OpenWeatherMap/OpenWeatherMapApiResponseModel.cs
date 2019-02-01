﻿namespace Connectors.OpenWeatherMap
{
    internal class OpenWeatherMapApiResponseModel
    {
        public Coord coord { get; set; }
        public Weather[] weather { get; set; }
        public string _base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }
    }

    public class Coord
    {
        public string lon { get; set; }
        public string lat { get; set; }
    }

    public class Main
    {
        public decimal temp { get; set; }
        public decimal pressure { get; set; }
        public decimal humidity { get; set; }
        public decimal temp_min { get; set; }
        public decimal temp_max { get; set; }
    }

    public class Wind
    {
        public decimal speed { get; set; }
        public decimal deg { get; set; }
    }

    public class Clouds
    {
        public int all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public decimal message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }

    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }
}