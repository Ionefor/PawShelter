using Microsoft.AspNetCore.Mvc;
using PawShelter.API.Extensions;
using PawShelter.Application.Species.AddBreed;
using PawShelter.Application.Species.AddSpecies;

namespace PawShelter.API.Controllers;

public class SpeciesController : ApplicationController
{
    [HttpPost]
    public async Task<ActionResult> AddSpecies(
        [FromBody] AddSpeciesRequest request,
        [FromServices] AddSpeciesHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
            
        if(result.IsFailure)
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
            
        if(result.IsFailure)
            return result.Error.ToResponse();
            
        return Ok(result.Value);
    }
    
}