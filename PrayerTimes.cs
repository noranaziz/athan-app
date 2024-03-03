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
                try {
                    PrayerTimesResponse? prayerTimes = JsonConvert.DeserializeObject<PrayerTimesResponse>(result);

                    // print out specific prayer times
                    PrintPrayerTimes(prayerTimes);
                } catch(JsonException ex) {
                    Console.WriteLine($"Error during deserialization: {ex.Message}");
                }
            } else {
                Console.WriteLine($"Error: {response.StatusCode}");
            }
        }
    }

    // method to print prayer times
    static void PrintPrayerTimes(PrayerTimesResponse prayerTimes) {
        if(prayerTimes != null && prayerTimes.data != null && prayerTimes.data.timings != null) {
            var timings = prayerTimes.data.timings;

            Console.WriteLine($"Fajr: {timings.Fajr}");
            Console.WriteLine($"Dhuhr: {timings.Dhuhr}");
            Console.WriteLine($"Asr: {timings.Asr}");
            Console.WriteLine($"Maghreb: {timings.Maghrib}");
            Console.WriteLine($"Isha: {timings.Isha}");
        } else {
            Console.WriteLine("Error: Unable to print prayer times. Data is null or incomplete.");
        }
    }
}