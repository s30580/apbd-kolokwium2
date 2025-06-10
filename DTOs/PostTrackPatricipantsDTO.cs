namespace apbd_kolowium2.DTOs;

public class PostTrackPatricipantsDTO
{
    public string RaceName { get; set; }
    public string TrackName { get; set; }
    public ICollection<PTParticipantsDTO> Participations { get; set; }
    
}

public class PTParticipantsDTO
{
    public int RacerId { get; set; }
    public int Position { get; set; }
    public int FinishTimeInSeconds { get; set; }
}