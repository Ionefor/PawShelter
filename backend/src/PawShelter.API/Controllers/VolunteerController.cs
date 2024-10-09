using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PawShelter.API.Extensions;
using PawShelter.Application.Volunteers.CreateVolunteer;
using PawShelter.Domain.Shared;

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
            
            //Возвращает код 500 с такой ошибкой
            //CSharpFunctionalExtensions.ResultSuccessException:
            //You attempted to access the Error property for a successful result.
            //A successful result has no Error.
            //Пока не совсем понятно, что с этим сделать
            //??
            //return result.ToResponse()
            return Ok(result);
        }
    }
}
