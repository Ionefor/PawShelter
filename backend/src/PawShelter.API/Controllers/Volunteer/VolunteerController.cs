using Microsoft.AspNetCore.Mvc;
using PawShelter.API.Controllers.Volunteer.Requests;
using PawShelter.API.Processors;
using PawShelter.Application.Volunteers.Queries.GetVolunteerById;
using PawShelter.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PawShelter.Application.Volunteers.UseCases.AddPet;
using PawShelter.Application.Volunteers.UseCases.AddPetPhotos;
using PawShelter.Application.Volunteers.UseCases.Create;
using PawShelter.Application.Volunteers.UseCases.Delete;
using PawShelter.Application.Volunteers.UseCases.UpdateMainInfo;
using PawShelter.Application.Volunteers.UseCases.UpdateRequisites;
using PawShelter.Application.Volunteers.UseCases.UpdateSocialNetworks;

namespace PawShelter.API.Controllers.Volunteer;

public class VolunteerController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromServices] CreateVolunteerHandler handler,
        [FromBody] CreateVolunteerRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
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

        if (result.IsFailure)
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

        if (result.IsFailure)
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

        if (result.IsFailure)
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

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPost("{id:guid}/pet")]
    public async Task<ActionResult<Guid>> AddPet(
        [FromRoute] Guid id,
        [FromServices] AddPetHandler handler,
        [FromBody] AddPetRequest request,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpPost("{volunteerId:guid}/pet/{petId:guid}")]
    public async Task<ActionResult<Guid>> AddPetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] AddPetPhotosHandler handler,
        [FromForm] AddPetPhotosRequest request,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var filesDto = fileProcessor.Process(request.Files);

        var command = request.ToCommand(volunteerId, petId, filesDto);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(Envelope.Ok(result.Value));
    }

    [HttpGet]
    public async Task<ActionResult<Guid>> GetAll(
        [FromServices] GetVolunteersWithPaginationHandler handler,
        [FromQuery] GetVolunteersWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }
    
    [HttpGet("{volunteerId:guid}")]
    public async Task<ActionResult<Guid>> GetById(
        [FromServices] GetVolunteerByIdHandler handler,
        [FromRoute] Guid volunteerId,
        CancellationToken cancellationToken)
    {
        var query = new GetVolunteerByIdQuery(volunteerId);
    
        var result = await handler.Handle(query, cancellationToken);
        
        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));
        
        return Ok(result.Value);
    }
}
