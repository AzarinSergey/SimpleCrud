namespace Gis.Domain.External.Provider.Model
{
    //{"country":"Netherlands",
    //"city_iata":"AMS",
    //"iata":"AMS",
    //"city":"Amsterdam",
    //"timezone_region_name":"Europe/Amsterdam",
    //"country_iata":"NL",
    //"rating":3,
    //"name":"Amsterdam",
    //"location":{"lon":4.763385,"lat":52.309069},"type":"airport","hubs":7}
    public class AirportPosition
    {
        public string Name { get; set; }
        public string Iata { get; set; }
        public Location Location { get; set; }
    }

    public class Location
    {
        public double Lat { get; set; }
        public double Lon { get; set; }
    }
}