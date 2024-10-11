using Microsoft.AspNetCore.Mvc;
using PawShelter.Application.Volunteers.CreateVolunteer;

namespace PawShelter.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VolunteerController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> Create(
            [FromServices] CreateVolunteerHandler handler,
            [FromBody] CreateVolunteerRequest request,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(), cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(Envelope.Error(result.Error));
            
            return Created("", Envelope.Ok(result.Value));
        }
    }
}
