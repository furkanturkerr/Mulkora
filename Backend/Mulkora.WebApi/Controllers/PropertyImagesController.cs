using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mulkora.Business.Abstract;
using Mulkora.Dto.PropertyImageDtos;
using Mulkora.WebApi.Services;

namespace Mulkora.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Agent")]
public class PropertyImagesController : ControllerBase
{
    private readonly ImageFileService _imageFileService;
    private readonly IPropertyImageService _propertyImageService;

    public PropertyImagesController(ImageFileService imageFileService, IPropertyImageService propertyImageService)
    {
        _imageFileService = imageFileService;
        _propertyImageService = propertyImageService;
    }

    [HttpPost("{propertyId:int}")]
    public async Task<IActionResult> UploadImages(int propertyId, [FromForm] List<IFormFile> images)
    {
        var agentIdClaim = User.FindFirstValue("AgentId");

        if (!int.TryParse(agentIdClaim, out var agentId))
        {
            return Unauthorized();
        }

        if (images == null || images.Count == 0)
        {
            return BadRequest("Görsel seçilmedi.");
        }

        await _propertyImageService.TCheckPropertyOwnerAsync(propertyId, agentId);

        var imageUrls = await _imageFileService.SaveImagesAsync(images);

        var dto = new CreatePropertyImagesDto
        {
            PropertyId = propertyId,
            ImageUrls = imageUrls
        };

        await _propertyImageService.TInsertImagesAsync(dto);

        return Ok();
    }
    
    [HttpGet("property/{propertyId:int}")]
    public async Task<IActionResult> GetImagesByPropertyId(int propertyId)
    {
        var images = await _propertyImageService.TGetImagesByPropertyIdAsync(propertyId);

        return Ok(images);
    }
    
    [HttpDelete("{imageId:int}")]
    [Authorize(Roles = "Agent")]
    public async Task<IActionResult> Delete(int imageId)
    {
        var agentIdClaim = User.FindFirstValue("AgentId");

        if (!int.TryParse(agentIdClaim, out var agentId))
        {
            return Unauthorized();
        }

        var imageUrl = await _propertyImageService.TDeleteImageAsync(imageId, agentId);

        _imageFileService.DeleteImage(imageUrl);

        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpGet("public/property/{propertyId:int}")]
    public async Task<IActionResult> GetPublicImagesByPropertyId(int propertyId)
    {
        var images = await _propertyImageService.TGetPublicImagesByPropertyIdAsync(propertyId);

        return Ok(images);
    }
}