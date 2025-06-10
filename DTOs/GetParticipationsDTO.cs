namespace apbd_kolowium2.DTOs;

public class GetParticipationsDTO
{
    public int RacerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public ICollection<GPParticipationDTO> Participations { get; set; }
}

public class GPParticipationDTO
{
    public GPRaceDTO Race { get; set; }
    public GPTrackDTO Track { get; set; }
    public int Laps { get; set; }
    public int FinishTimeInSeconds { get; set; }
    public int Position { get; set; }
}

public class GPRaceDTO
{
    public string Name { get; set; }
    public string Location { get; set; }
    public DateTime Date { get; set; }
}

public class GPTrackDTO
{
    public string Name { get; set; }
    public decimal LengthInKm { get; set; }
}
