using System;
using System.Net.Http; // for http requests
using System.Threading.Tasks; // for async programming

class PrayerTimes {
    static async Task Main() {
        // ask user for city and country
        Console.WriteLine("Enter your city: ");
        string city = Console.ReadLine();
        Console.WriteLine("Enter your country: ");
        string country = Console.ReadLine();

        string baseURL = "http://api.aladhan.com/v1/";

        using (HttpClient client = new HttpClient()) {
            // fetch prayer times for a specific city
            string endpoint = $"timingsByCity?city={city}&country={country}";
            HttpResponseMessage response = await client.GetAsync(baseURL + endpoint);

            // check if response worked
            if(response.IsSuccessStatusCode) {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);
            } else {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}