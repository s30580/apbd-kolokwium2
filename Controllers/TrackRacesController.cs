using apbd_kolowium2.DTOs;
using apbd_kolowium2.Exceptions;
using apbd_kolowium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_kolowium2.Controllers;
[ApiController]
[Route("api/track-races")]
public class TrackRacesController : ControllerBase
{
    private readonly ITrackRacesService _trackRacesService;

    public TrackRacesController(ITrackRacesService trackRacesService)
    {
        _trackRacesService = trackRacesService;
    }

    [HttpPost("participants")]
    public async Task<IActionResult> PostTrackParticipants([FromBody] PostTrackPatricipantsDTO postTrackPatricipantsDTO)
    {
        try
        {
            await _trackRacesService.PostTrackParticipants(postTrackPatricipantsDTO);
            return NoContent();
        }
        catch (NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
}