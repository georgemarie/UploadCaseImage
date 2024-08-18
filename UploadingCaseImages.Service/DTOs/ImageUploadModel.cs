using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace UploadingCaseImages.Service.DTOs;
public class ImageUploadModel
{
	public List<IFormFile> Files { get; set; }
}
