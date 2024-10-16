using Microsoft.AspNetCore.Mvc;
using PawShelter.API.Extensions;
using PawShelter.Application.FileProvider;
using PawShelter.Application.Files.Delete;
using PawShelter.Application.Files.Get;
using PawShelter.Application.Files.Upload;
using PawShelter.Domain.PetsManagement.ValueObjects.ForPet;

namespace PawShelter.API.Controllers
{
    public class FileController : ApplicationController
    {
       [HttpPost]
        public async Task<ActionResult> Upload(
            IFormFile file,
            [FromServices] UploadFileHandler fileHandler,
            CancellationToken cancellationToken)
        {
            await using var stream = file.OpenReadStream();
            
            var path = Guid.NewGuid().ToString();
            
            var command = new UploadFileCommand(stream, "photos", path);
            
            var result = await fileHandler.Handle(command, cancellationToken);
            
            if(result.IsFailure)return 
                result.Error.ToResponse();
            
            return Ok(result.Value);
        }
    
        [HttpDelete("{objectName:guid}")]
        public async Task<ActionResult> Delete(
            [FromRoute] Guid objectName,
            [FromServices] DeleteFileHandler fileHandler,
            CancellationToken cancellationToken)
        {
            var fileData = new FileMetaData("photos", FilePath.Create(objectName.ToString()).Value);
            var command = new DeleteFileCommand(fileData);
            
            var result = await fileHandler.Handle(command, cancellationToken);
            
            if(result.IsFailure)return 
                result.Error.ToResponse();
            
            return Ok();
        }
        
        [HttpGet]
        public async Task<ActionResult> GetFile(
             Guid objectName,
            [FromServices] GetFileHandler handler,
            CancellationToken cancellationToken)
        {
            var fileData = new FileMetaData("photos", FilePath.Create(objectName.ToString()).Value);
            var command = new GetFileCommand(fileData);
            
            var result = await handler.Handle(command, cancellationToken);
            
            if(result.IsFailure)
                return result.Error.ToResponse();
            
            return Ok(result.Value);
        }
    }  
}
    


