using Microsoft.AspNetCore.Mvc;
using PawShelter.Core.Models;
using PawShelter.Core.Processors;
using PawShelter.Framework;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.AddPet;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.DeletePetPhoto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.HardDeletePet;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.SetMainPetPhoto;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.SoftDeletePet;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePet;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetPhotos;
using PawShelter.Volunteers.Application.Volunteers.Commands.Pet.UpdatePetStatus;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Create;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.Delete;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateMainInfo;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateRequisites;
using PawShelter.Volunteers.Application.Volunteers.Commands.Volunteer.UpdateSocialNetworks;
using PawShelter.Volunteers.Application.Volunteers.Queries.GetFilteredPetsWithPagination;
using PawShelter.Volunteers.Application.Volunteers.Queries.GetPetById;
using PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteerById;
using PawShelter.Volunteers.Application.Volunteers.Queries.GetVolunteersWithPagination;
using PawShelter.Volunteers.Presentation.Requests;

namespace PawShelter.Volunteers.Presentation;

public class VolunteerController : ApplicationController
{
    [Permission(Permissions.Volunteers.Create)]
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
    
    [Permission(Permissions.Volunteers.Update)]
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

    [Permission(Permissions.Volunteers.Update)]
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

    [Permission(Permissions.Volunteers.Update)]
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
    
    [Permission(Permissions.Pets.Update)]
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}/status")]
    public async Task<ActionResult<Guid>> UpdatePetStatus(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromBody] UpdatePetStatusRequest request,
        [FromServices] UpdatePetStatusHandler handler,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);
        
        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(result.Value);
    }
    
    [Permission(Permissions.Volunteers.Delete)]
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
    
    [Permission(Permissions.Pets.Delete)]
    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/photos")]
    public async Task<ActionResult<string>> DeletePetPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] DeletePetPhotoHandler handler,
        [FromForm] DeletePetPhotoRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.Delete)]
    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}/soft")]
    public async Task<ActionResult<Guid>> SoftDeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SoftDeletePetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new SoftDeletePetCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(result.Value);
    }
    
    [Permission(Permissions.Pets.Delete)]
    [HttpDelete("{volunteerId:guid}/pet/{petId:guid}")]
    public async Task<ActionResult<Guid>> DeletePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] HardDeletePetHandler handler,
        CancellationToken cancellationToken)
    {
        var command = new HardDeletePetCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(result.Value);
    }
    
    [Permission(Permissions.Pets.Create)]
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

    [Permission(Permissions.Pets.Update)]
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}/photos")]
    public async Task<ActionResult<Guid>> SetMainPhoto(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] SetMainPetPhotoHandler handler,
        [FromForm] SetMainPetPhotoRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(result.Value);
    }

    [Permission(Permissions.Pets.Update)]
    [HttpPost("{volunteerId:guid}/pet/{petId:guid}")]
    public async Task<ActionResult<Guid>> UpdatePetPhotos(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetPhotosHandler handler,
        [FromForm] UpdatePetPhotosRequest request,
        CancellationToken cancellationToken)
    {
        await using var fileProcessor = new FormFileProcessor();
        var filesDto = fileProcessor.Process(request.Files);

        var command = request.ToCommand(volunteerId, petId, filesDto);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(result.Value);
    }
    
    [Permission(Permissions.Pets.Update)]
    [HttpPut("{volunteerId:guid}/pet/{petId:guid}")]
    public async Task<ActionResult<Guid>> UpdatePet(
        [FromRoute] Guid volunteerId,
        [FromRoute] Guid petId,
        [FromServices] UpdatePetHandler handler,
        [FromBody] UpdatePetRequest request,
        CancellationToken cancellationToken)
    {
        var command = request.ToCommand(volunteerId, petId);

        var result = await handler.Handle(command, cancellationToken);

        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));

        return Ok(result.Value);
    }
    
    [Permission(Permissions.Volunteers.Read)]
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
    
    [Permission(Permissions.Volunteers.Read)]
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
    
    [Permission(Permissions.Pets.Read)]
    [HttpGet("/pets")]
    public async Task<ActionResult<Guid>> GetAllPet(
        [FromServices] GetFilteredPetsWithPaginationHandler handler,
        [FromQuery] GetFilteredPetsWithPaginationRequest request,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();

        var response = await handler.Handle(query, cancellationToken);

        return Ok(response);
    }
    
    [Permission(Permissions.Pets.Read)]
    [HttpGet("/pets/{petId:guid}")]
    public async Task<ActionResult<Guid>> GetPetById(
        [FromRoute] Guid petId,
        [FromServices] GetPetByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var query = new GetPetByIdQuery(petId);
    
        var result = await handler.Handle(query, cancellationToken);
        
        if (result.IsFailure)
            return BadRequest(Envelope.Error(result.Error));
        
        return Ok(result.Value);
    }
}
