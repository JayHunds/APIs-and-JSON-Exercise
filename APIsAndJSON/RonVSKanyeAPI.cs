using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIsAndJSON
{
    public class RonVSKanyeAPI
    { 
        public static async Task APIconverse(string[] args)
    {
        using var httpClient = new HttpClient();

        string ronSwansonAPI = "https://ron-swanson-quotes.herokuapp.com/v2/quotes";

        string kanyeAPI = "https://api.kanye.rest";

        for (int i = 0; i < 5; i++)
        {
            string ronSwansonQuote = await FetchQuote(httpClient, ronSwansonAPI);

            string kanyeQuote = await FetchKanyeQuote(httpClient, kanyeAPI);

            Console.WriteLine($"Ron Swanson: {ronSwansonQuote}");
            Console.WriteLine($"Kanye: {kanyeQuote}");
        }
    }

   public static async Task<string> FetchQuote(HttpClient httpClient, string apiUrl)
    {
        string quote = "";

        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                quote = await response.Content.ReadAsStringAsync();
                quote = JsonConvert.DeserializeObject<string[]>(quote)[0];
            }
            else
            {
                Console.WriteLine($"Failed to fetch quote from {apiUrl}: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching quote from {apiUrl}: {ex.Message}");
        }

        return quote;
    }

    static async Task<string> FetchKanyeQuote(HttpClient httpClient, string apiUrl)
    {
        string quote = "";

        try
        {
            HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                quote = JsonConvert.DeserializeObject<dynamic>(json).quote;
            }
            else
            {
                Console.WriteLine($"Failed to fetch quote from {apiUrl}: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while fetching quote from {apiUrl}: {ex.Message}");
        }

        return quote;
    }
}
}
