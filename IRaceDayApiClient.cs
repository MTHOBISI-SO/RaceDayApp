using RaceDayWeb.Models;

namespace RaceDayWeb.Services;

public interface IRaceDayApiClient
{
    Task<List<Race>> GetRacesAsync();
    Task<Race?> GetRaceAsync(int id);
    Task<List<Venue>> GetVenuesAsync();
    Task<bool> RegisterForRaceAsync(int raceId, string token);
    Task<List<Registration>> GetRegistrationsAsync(string token);
    Task<(bool Success, string? Token, string? Role, string? Username, string? Error)> LoginAsync(string username, string password);
    Task<(bool Success, string? Token, string? Role, string? Username, string? Error)> RegisterAsync(RegisterViewModel model);
}
