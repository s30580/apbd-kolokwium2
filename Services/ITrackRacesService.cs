using apbd_kolowium2.DTOs;

namespace apbd_kolowium2.Services;

public interface ITrackRacesService
{
    Task PostTrackParticipants(PostTrackPatricipantsDTO participants);
}