using Microsoft.AspNetCore.Mvc;
using PawShelter.Application.Volunteers.Create;
using PawShelter.Application.Volunteers.CreateVolunteer;
using PawShelter.Application.Volunteers.Delete;
using PawShelter.Application.Volunteers.UpdateMainInfo;
using PawShelter.Application.Volunteers.UpdateRequisites;
using PawShelter.Application.Volunteers.UpdateSocialNetworks;

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
        
        [HttpPut("{id:guid}/main-info")]
        public async Task<ActionResult<Guid>> UpdateMainInfo(
            [FromRoute] Guid id,
            [FromBody] UpdateMainInfoRequest request,
            [FromServices] UpdateMainInfoHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(id), cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(Envelope.Error(result.Error));

            return Ok(Envelope.Ok(result.Value));
        }
        
        [HttpPut("{id:guid}/requisites")]
        public async Task<ActionResult<Guid>> UpdateRequisites(
            [FromRoute] Guid id,
            [FromBody] UpdateRequisitesRequest request,
            [FromServices] UpdateRequisitesHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(id), cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(Envelope.Error(result.Error));

            return Ok(Envelope.Ok(result.Value));
        }
        
        [HttpPut("{id:guid}/socialNetworks")]
        public async Task<ActionResult<Guid>> UpdateSocialNetworks(
            [FromRoute] Guid id,
            [FromBody] UpdateSocialNetworksRequest request,
            [FromServices] UpdateSocialNetworksHandler handler,
            CancellationToken cancellationToken)
        {
            var result = await handler.Handle(request.ToCommand(id), cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(Envelope.Error(result.Error));

            return Ok(Envelope.Ok(result.Value));
        }
        
        [HttpDelete("{id:guid}/volunteer")]
        public async Task<ActionResult<Guid>> Delete(
            [FromRoute] Guid id,
            [FromServices] DeleteVolunteerHandler handler,
            CancellationToken cancellationToken)
        {
            var request = new DeleteVolunteerRequest(id);
            
            var result = await handler.Handle(request.ToCommand(), cancellationToken);
            
            if(result.IsFailure)
                return BadRequest(Envelope.Error(result.Error));

            return Ok(Envelope.Ok(result.Value));
        }
    }
}
