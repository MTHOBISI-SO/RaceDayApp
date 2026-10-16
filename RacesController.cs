using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaceDayWeb.Services;

namespace RaceDayWeb.Controllers;

public class RacesController : Controller
{
    private readonly IRaceDayApiClient _api;

    public RacesController(IRaceDayApiClient api) => _api = api;

    public async Task<IActionResult> Index()
    {
        return View(await _api.GetRacesAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var race = await _api.GetRaceAsync(id);
        return race is null ? NotFound() : View(race);
    }

    [Authorize(Roles = "Participant")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(int raceId)
    {
        var token = HttpContext.Session.GetString("JwtToken");
        if (string.IsNullOrWhiteSpace(token))
            return RedirectToAction("Login", "Account");

        var success = await _api.RegisterForRaceAsync(raceId, token);

        TempData["Message"] = success
            ? "You have been successfully registered for the race."
            : "Registration failed. You may already be registered.";

        return RedirectToAction(nameof(Details), new { id = raceId });
    }
}
