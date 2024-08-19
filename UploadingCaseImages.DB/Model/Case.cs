using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UploadingCaseImages.DB.Model
{
	public class Case
	{
		[Key]
		public int Id { get; set; }
		public int PatientId { get; set; }
		public DateTime CaseDate { get; set; }
		public int AnatomyId { get; set; }
		public string DoctorNotes { get; set; }
		public ICollection<AnatomyModel> ImageUrls { get; set; }
	}
}
