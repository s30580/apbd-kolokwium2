using apbd_kolowium2.DTOs;

namespace apbd_kolowium2.Services;

public interface IRaceService
{
    Task<GetParticipationsDTO> GetParticipationsAsync(int id);
}