using apbd_kolowium2.Data;
using apbd_kolowium2.DTOs;
using apbd_kolowium2.Exceptions;
using apbd_kolowium2.Models;

namespace apbd_kolowium2.Services;

public class TrackRacesService:ITrackRacesService
{
    private readonly DatabaseContext _context;

    public TrackRacesService(DatabaseContext context)
    {
        _context = context;
    }


    public async Task PostTrackParticipants(PostTrackPatricipantsDTO participants)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var race = _context.Races.FirstOrDefault(e => e.Name.Equals(participants.RaceName));
            if (race == null) throw new NotFoundException("Race not found");

            var track = _context.Tracks.FirstOrDefault(e => e.Name.Equals(participants.TrackName));
            if (track == null) throw new NotFoundException("Track not found");

            foreach (var participation in participants.Participations)
            {
                var racer = _context.Racers.FirstOrDefault(e=>e.RacerId == participation.RacerId);
                if (racer == null) throw new NotFoundException("Racer not found");

                var raceParticipation = new RaceParticipation
                {
                    TrackRaceId = track.TrackId,
                    RacerId = racer.RacerId,
                    FinishTimeInSeconds = participation.FinishTimeInSeconds,
                    Position = participation.Position,
                };
                
                var trackRace = _context.TrackRaces.FirstOrDefault(e=>e.TrackId == track.TrackId && e.RaceId == race.RaceId);
                if (trackRace == null) throw new NotFoundException("Track Race not found");

                if (participation.FinishTimeInSeconds < trackRace.BestTimeInSeconds)
                {
                    trackRace.BestTimeInSeconds = participation.FinishTimeInSeconds;
                    _context.Update(trackRace);
                }
                await _context.RaceParticipations.AddAsync(raceParticipation);
                await _context.SaveChangesAsync();
            }
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
            throw;
        }
        
        
    }
}