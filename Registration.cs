namespace RaceDayWeb.Models;

public class Registration
{
    public int RegistrationID { get; set; }
    public int ParticipantID { get; set; }
    public int RaceID { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string RegistrationStatus { get; set; } = "";
    public Race? Race { get; set; }
}
