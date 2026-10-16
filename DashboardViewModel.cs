namespace RaceDayWeb.Models;

public class DashboardViewModel
{
    public List<Race> UpcomingRaces { get; set; } = new();
    public List<Venue> Venues { get; set; } = new();
}
