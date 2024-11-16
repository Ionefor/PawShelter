using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PawShelter.Framework;
using PawShelter.Framework.Extensions;
using PawShelter.Species.Application.Species.Commands.AddBreed;
using PawShelter.Species.Application.Species.Commands.AddSpecies;
using PawShelter.Species.Application.Species.Commands.DeleteBreed;
using PawShelter.Species.Application.Species.Commands.DeleteSpecies;
using PawShelter.Species.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;
using PawShelter.Species.Application.Species.Queries.GetSpeciesWithPagination;
using PawShelter.Species.Presentation.Requests;

namespace PawShelter.Species.Presentation;

public class SpeciesController : ApplicationController
{
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> AddSpecies(
        [FromBody] AddSpeciesRequest request,
        [FromServices] AddSpeciesHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpPost("{id:guid}/breed")]
    public async Task<ActionResult> AddBreed(
        [FromRoute] Guid id,
        [FromBody] AddBreedRequest request,
        [FromServices] AddBreedHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(id), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteSpecies(
        [FromRoute] Guid id,
        [FromServices] DeleteSpeciesHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new DeleteSpeciesRequest(id);
        
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [Authorize]
    [HttpDelete("{speciesId:guid}/breed/{breedId:guid}")]
    public async Task<ActionResult> DeleteBreed(
        [FromRoute] Guid speciesId,
        [FromRoute] Guid breedId,
        [FromServices] DeleteBreedHandler handler,
        CancellationToken cancellationToken)
    {
        var request = new DeleteBreedRequest(speciesId, breedId);
        
        var result = await handler.Handle(request.ToCommand(), cancellationToken);

        if (result.IsFailure)
            return result.Error.ToResponse();

        return Ok(result.Value);
    }
    
    [HttpGet]
    public async Task<ActionResult> GetSpecies(
        [FromQuery] GetSpeciesWithPaginationRequest request,
        [FromServices] GetSpeciesWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery();
        
        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }
    
    [HttpGet("{speciesId:guid}/breed/")]
    public async Task<ActionResult> GetBreeds(
        [FromRoute] Guid speciesId,
        [FromQuery] GetBreedsBySpeciesIdWithPaginationRequest request,
        [FromServices] GetBreedsBySpeciesIdWithPaginationHandler handler,
        CancellationToken cancellationToken)
    {
        var query = request.ToQuery(speciesId);
        
        var response = await handler.Handle(query, cancellationToken);
        
        return Ok(response);
    }
    
}