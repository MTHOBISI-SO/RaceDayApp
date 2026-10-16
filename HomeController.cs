using Microsoft.AspNetCore.Mvc;
using RaceDayWeb.Models;
using RaceDayWeb.Services;

namespace RaceDayWeb.Controllers;

public class HomeController : Controller
{
    private readonly IRaceDayApiClient _api;

    public HomeController(IRaceDayApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        var races = await _api.GetRacesAsync();
        var venues = await _api.GetVenuesAsync();

        var model = new DashboardViewModel
        {
            UpcomingRaces = races
                .Where(r => r.RaceDate >= DateTime.Today)
                .OrderBy(r => r.RaceDate)
                .Take(6)
                .ToList(),
            Venues = venues
        };

        return View(model);
    }

    public IActionResult Privacy() => View();
}
