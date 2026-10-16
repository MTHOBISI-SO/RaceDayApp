namespace RaceDayWeb.Models;

public class Venue
{
    public int VenueID { get; set; }
    public string VenueName { get; set; } = "";
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public int Capacity { get; set; }
}
