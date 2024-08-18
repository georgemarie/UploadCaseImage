using AutoMapper;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Service.Profiles
{
	public class MyMapper : Profile
	{
		public MyMapper()
		{
			// Anatomy and BodyImageDto mappings
			CreateMap<Anatomy, GetPatientCaseDto>()
				.ReverseMap(); // If you want two-way mapping

			// UpdateBodyImageDto to Anatomy mapping
			CreateMap<GetPatientCaseDto, Anatomy>();

			// Assuming CaseImage is a model and CaseImageDto is its DTO
			CreateMap<CaseImage, GetPatientCaseDto>()
				.ReverseMap(); // If you want two-way mapping

			// PatientCase mapping to DTO and vice versa
			CreateMap<PatientCase, GetPatientCaseDto>()
				.ReverseMap(); // If you want two-way mapping

			// You can add other mappings here as needed
		}
	}
}
