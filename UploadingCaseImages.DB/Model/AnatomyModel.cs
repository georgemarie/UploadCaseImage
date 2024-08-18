using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Enums;

namespace UploadingCaseImages.DB.Model;
public class AnatomyModel
{
	[Key]
	public int AnatomyId { get; set; }
	public string ImagePath { get; set; }
	public AnatomyEnum BodyPart { get; set; }
}
