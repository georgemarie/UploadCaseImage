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

			CreateMap<CaseImage, CaseImageToReturnDto>();

			CreateMap<Anatomy, AnatomyDto>();

			CreateMap<PatientCase, PatientCaseToReturnDto>()
			.ForMember(dest => dest.AnatomyId, opt => opt.MapFrom(src => src.Anatomy.AnatomyId))
			.ForMember(dest => dest.AnatomyName, opt => opt.MapFrom(src => src.Anatomy.AnatomyName))
			.ForMember(dest => dest.CaseImages, opt => opt.MapFrom(src => src.CaseImages));
		}
		// You can add other mappings here as needed
	}
	
}
