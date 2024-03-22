using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OpenWeatherMap;
namespace APIsAndJSON
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await RonVSKanyeAPI.APIconverse(args);

            await OpenWeatherMapAPI.Weather(args);
        }
    }

}





