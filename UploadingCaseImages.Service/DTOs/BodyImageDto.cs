using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Enums;

namespace UploadingCaseImages.Service.DTOs;
public class BodyImageDto
{
	public int Id { get; set; }
	public string ImagePath { get; set; }
	public BodyPart BodyPart { get; set; }
}
