using GeneratedWorlds.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GeneratedWorlds.Infrastructure.Services
{
    public class OpenAIPotionGenerator : IPotionGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public OpenAIPotionGenerator(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"];
        }

        public async Task<(string name, string effect)> GeneratePotionAsync(int skillLevel)
        {
            var prompt = $"Generate a potion for a fantasy RPG with brewery skill level {skillLevel}. Return JSON with 'name' and 'effect'.";

            var requestBody = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.8
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
            request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            request.Content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadFromJsonAsync<JsonElement>();
            var content = json
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            // Try to parse name/effect from returned content
            var result = JsonSerializer.Deserialize<Dictionary<string, string>>(content ?? "{}");

            return (result?.GetValueOrDefault("name") ?? "Unknown Elixir", result?.GetValueOrDefault("effect") ?? "No effect");
        }
    }
}
