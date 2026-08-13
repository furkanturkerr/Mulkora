using Microsoft.AspNetCore.Mvc.Rendering;
using Mulkora.Dto.PropertyDtos;
using Mulkora.WebUI.Models;

namespace Mulkora.WebUI.Areas.Admin.Models;

public class UpdatePropertyViewModel
{ 
       public UpdatePropertyDto Property { get; set; } = new();
   
       public List<FeatureOptionViewModel> Features { get; set; } = new();
}