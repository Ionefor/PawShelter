using Microsoft.AspNetCore.Mvc;
using PawShelter.API.Extensions;
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

            return result.ToResponse();
        }
    }
}
