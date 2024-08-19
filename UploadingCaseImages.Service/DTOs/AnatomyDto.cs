using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadingCaseImages.Service.DTOs;
public class AnatomyDto
{
	public int AnatomyId { get; set; }
	public string AnatomyName { get; set; }
	public string ImagePath { get; set; }
	public DateTime CreatedAt { get; set; }
}
