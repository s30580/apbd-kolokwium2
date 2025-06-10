using apbd_kolowium2.Data;
using apbd_kolowium2.DTOs;
using apbd_kolowium2.Exceptions;
using apbd_kolowium2.Models;
using Microsoft.EntityFrameworkCore;

namespace apbd_kolowium2.Services;

public class RaceService:IRaceService
{
    private readonly DatabaseContext _context;

    public RaceService(DatabaseContext context)
    {
        _context = context;
    }


    public async Task<GetParticipationsDTO> GetParticipationsAsync(int id)
    {
        var racer = _context.Racers
            .Include(e=>e.RaceParticipations)
            .ThenInclude(e=>e.TrackRace)
            .ThenInclude(e=>e.Track)
            .Include(e=>e.RaceParticipations)
            .ThenInclude(e=>e.TrackRace)
            .ThenInclude(e=>e.Race)
            .FirstOrDefault(e=>e.RacerId==id);
        if (racer==null)throw new NotFoundException("Racer not found");
        
        return new GetParticipationsDTO
        {
            RacerId=racer.RacerId,
            FirstName = racer.FirstName,
            LastName = racer.LastName,
            Participations = racer.RaceParticipations.Select(e => new GPParticipationDTO()
            {
                Race = new GPRaceDTO()
                {
                    Name = e.TrackRace.Race.Name,
                    Location = e.TrackRace.Race.Location,
                    Date = e.TrackRace.Race.Date
                },
                Track = new GPTrackDTO()
                {
                    Name = e.TrackRace.Track.Name,
                    LengthInKm = e.TrackRace.Track.LengthInKm,
                },
                Laps = e.TrackRace.Laps,
                FinishTimeInSeconds = e.FinishTimeInSeconds,
                Position = e.Position
            }).ToList()
        };
        
        
    }
}