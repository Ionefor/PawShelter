using Microsoft.AspNetCore.Mvc;
using PawShelter.Accounts.Application.Command.Login;
using PawShelter.Accounts.Application.Command.Register;
using PawShelter.Accounts.Presentation.Requests;
using PawShelter.Framework;
using PawShelter.Framework.Extensions;

namespace PawShelter.Accounts.Presentation;

public class AccountController : ApplicationController
{
    [HttpPost("registration")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        [FromServices] RegisterUserHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok();
    }
    
   
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginUserRequest request,
        [FromServices] LoginHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.Handle(request.ToCommand(), cancellationToken);
        if (result.IsFailure)
            return result.Error.ToResponse();
        
        return Ok(result.Value);
    }
}