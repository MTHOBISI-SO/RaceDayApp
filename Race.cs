namespace RaceDayWeb.Models;

public class Race
{
    public int RaceID { get; set; }
    public string RaceName { get; set; } = "";
    public DateTime RaceDate { get; set; }
    public string RaceType { get; set; } = "";
    public string Status { get; set; } = "";
    public int VenueID { get; set; }
    public Venue? Venue { get; set; }
}
