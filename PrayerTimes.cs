using System;
using System.Net.Http; // for http requests
using System.Threading.Tasks; // for async programming
using Newtonsoft.Json; // for parsing json

class PrayerTimes {
    static async Task Main() {
        // ask user for city and country
        Console.WriteLine("Enter your city: ");
        string city = Console.ReadLine()!;
        Console.WriteLine("Enter your state: ");
        string state = Console.ReadLine()!;

        string baseURL = "http://api.aladhan.com/v1/";

        using (HttpClient client = new HttpClient()) {
            // fetch prayer times for a specific city
            string endpoint = $"timingsByCity?city={city}&state={state}&country=United+States";
            HttpResponseMessage response = await client.GetAsync(baseURL + endpoint);

            // check if response worked
            if(response.IsSuccessStatusCode) {
                string result = await response.Content.ReadAsStringAsync();
                Console.WriteLine(result);

                // if response worked, parse the result
                PrayerTimesResponse? prayerTimes = JsonConvert.DeserializeObject<PrayerTimesResponse>(result);

                // print out specific prayer times
                if(prayerTimes != null) {
                    Console.WriteLine($"Fajr: {prayerTimes.PrayerData?.PrayerTimings?.Fajr}");
                } else {
                    Console.WriteLine("Error: Unable to deserialize PrayerTimesResponse");
                }
            } else {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }
}