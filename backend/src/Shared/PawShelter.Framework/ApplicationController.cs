using Microsoft.AspNetCore.Mvc;
using PawShelter.Core.Models;

namespace PawShelter.Framework;

[ApiController]
[Route("[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}