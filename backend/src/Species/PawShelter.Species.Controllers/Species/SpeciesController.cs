using Microsoft.AspNetCore.Mvc;
using PawShelter.Web.Extensions;
using PawShelter.Application.Species.Queries;
using PawShelter.Application.Species.Queries.GetBreedsBySpeciesIdWithPagination;
using PawShelter.Application.Species.Queries.GetSpeciesWithPagination;
using PawShelter.Application.Species.UseCases.AddBreed;
using PawShelter.Application.Species.UseCases.AddSpecies;
using PawShelter.Application.Species.UseCases.DeleteBreed;
using PawShelter.Application.Species.UseCases.DeleteSpecies;
using PawShelter.Web.Controllers.Species.Requests;

namespace PawShelter.Web.Controllers.Species;

public class SpeciesController : ApplicationController
{
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