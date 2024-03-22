using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace OpenWeatherMap;
public class OpenWeatherMapAPI
{
    public static async Task Weather(string[] args)
    {
        string apiKey = "fad52fc2384b12af503b6c2abfc6fa7a";

        string city = "Parker";

        WeatherData weatherData = await GetCurrentWeather(apiKey, city);

        if (weatherData != null)
        {


            Console.WriteLine($"\nCurrent Weather in {city}:");
            Console.WriteLine($"Temperature: {weatherData.Main.Temp} °F");
            Console.WriteLine($"Feels Like: {weatherData.Main.FeelsLike} °F");
            Console.WriteLine($"Description: {weatherData.Weather[0].Description}");
            Console.WriteLine($"Humidity: {weatherData.Main.Humidity}%");
            Console.WriteLine($"Wind Speed: {weatherData.Wind.Speed} m/s");
        }
        else
        {
            Console.WriteLine($"Failed to fetch weather data for {city}.");
        }
    }

    static async Task<WeatherData> GetCurrentWeather(string apiKey, string city)
    {
        using var httpClient = new HttpClient();

        try
        {
            string apiUrl = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=imperial";

            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);
                return weatherData;
            }
            else
            {
                Console.WriteLine($"Failed to fetch weather data for {city}: {response.StatusCode}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching weather data: {ex.Message}");
            return null;
        }
    }
}

public class WeatherData
{
    public MainData Main { get; set; }
    public Weather[] Weather { get; set; }
    public WindData Wind { get; set; }
}

public class MainData
{
    public float Temp { get; set; }
    public float FeelsLike { get; set; }
    public float Humidity { get; set; }
}

public class Weather
{
    public string Description { get; set; }
}

public class WindData
{
    public float Speed { get; set; }
}



