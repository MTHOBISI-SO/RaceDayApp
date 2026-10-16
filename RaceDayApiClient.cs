using System.Net.Http.Headers;
using System.Net.Http.Json;
using RaceDayWeb.Models;

namespace RaceDayWeb.Services;

public class RaceDayApiClient : IRaceDayApiClient
{
    private readonly HttpClient _http;

    public RaceDayApiClient(HttpClient http) => _http = http;

    public async Task<List<Race>> GetRacesAsync()
    {
        return await _http.GetFromJsonAsync<List<Race>>("api/races") ?? new();
    }

    public async Task<Race?> GetRaceAsync(int id)
    {
        return await _http.GetFromJsonAsync<Race>($"api/races/{id}");
    }

    public async Task<List<Venue>> GetVenuesAsync()
    {
        return await _http.GetFromJsonAsync<List<Venue>>("api/venues") ?? new();
    }

    public async Task<bool> RegisterForRaceAsync(int raceId, string token)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, "api/registrations")
        {
            Content = JsonContent.Create(new { raceID = raceId })
        };

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _http.SendAsync(request);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<Registration>> GetRegistrationsAsync(string token)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, "api/registrations");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _http.SendAsync(request);
        if (!response.IsSuccessStatusCode)
            return new();

        return await response.Content.ReadFromJsonAsync<List<Registration>>() ?? new();
    }

    public async Task<(bool Success, string? Token, string? Role, string? Username, string? Error)>
        LoginAsync(string username, string password)
    {
        var response = await _http.PostAsJsonAsync(
            "api/auth/login",
            new { username, password });

        if (!response.IsSuccessStatusCode)
            return (false, null, null, null, "Invalid username or password.");

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        return result is null
            ? (false, null, null, null, "The API returned an invalid response.")
            : (true, result.Token, result.Role, result.Username, null);
    }

    public async Task<(bool Success, string? Token, string? Role, string? Username, string? Error)>
        RegisterAsync(RegisterViewModel model)
    {
        if (model.Password != model.ConfirmPassword)
            return (false, null, null, null, "Passwords do not match.");

        var response = await _http.PostAsJsonAsync("api/auth/register", new
        {
            firstName = model.FirstName,
            lastName = model.LastName,
            dateOfBirth = model.DateOfBirth,
            email = model.Email,
            phone = model.Phone,
            username = model.Username,
            password = model.Password,
            role = model.Role
        });

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            return (false, null, null, null, message);
        }

        var result = await response.Content.ReadFromJsonAsync<AuthResponse>();
        return result is null
            ? (false, null, null, null, "The API returned an invalid response.")
            : (true, result.Token, result.Role, result.Username, null);
    }

    private class AuthResponse
    {
        public string Token { get; set; } = "";
        public string Role { get; set; } = "";
        public string Username { get; set; } = "";
    }
}
