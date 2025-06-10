using apbd_kolowium2.Exceptions;
using apbd_kolowium2.Services;
using Microsoft.AspNetCore.Mvc;

namespace apbd_kolowium2.Controllers;
[Route("api/[controller]")]
[ApiController]

public class RacersController : ControllerBase
{
    private readonly IRaceService _raceService;

    public RacersController(IRaceService raceService)
    {
        _raceService = raceService;
    }

    [HttpGet("{id}/participations")]
    public async Task<IActionResult> GetParticipations(int id)
    {
        try
        {
            return Ok(await _raceService.GetParticipationsAsync(id));
        }
        catch (NotFoundException e)
        {
            return NotFound();
        }
    }
}