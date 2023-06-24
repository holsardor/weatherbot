using weatherbot.Models;

namespace weatherbot
{
    public class WeatherService
    {
        private readonly HttpClient _openMeteoClient;
        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _openMeteoClient = httpClientFactory.CreateClient("OpenMeteoClient");
        }

        public async Task<string> GetWeatherTextAsync(double longitude, double latitude, CancellationToken cancellationToken = default)
        {
            var route = $"v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
            var weather = await _openMeteoClient.GetFromJsonAsync<WeatherResponse>(route, cancellationToken: cancellationToken);

            var weatherEmoji = weather.CurrentWeather.Temperature switch
            {
                > 40 => "🥵",
                > 30 => "♨️",
                > 20 => "☀️",
                > 15 => "⛅",
                > 10 => "🌥️",
                > 5 => "🌦️",
                > 0 => "🌨️",
                _ => "🥶",
            };

            return $"Hozirgi ob-havo: {weather.CurrentWeather.Temperature:F1} {weatherEmoji}";
        }
    }
}