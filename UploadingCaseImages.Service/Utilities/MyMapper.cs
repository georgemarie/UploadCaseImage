using AutoMapper;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service.Profiles
{
	public class MyMapper : Profile
	{
		public MyMapper()
		{
			CreateMap<Anatomy, AnatomyDto>();

			CreateMap<CaseImageToSaveDto, CaseImage>()
			.ReverseMap();

			CreateMap<PatientCaseToSave, PatientCase>()
			.ReverseMap();

			CreateMap<CaseImage, CaseImageToReturnDto>()
			.ReverseMap();

			CreateMap<PatientCase, PatientCaseToReturnDto>()
			.ForMember(d => d.AnatomyName, opt => opt.MapFrom(src => src.Anatomy.Name))
			.ForMember(d => d.CaseImages, opt => opt.MapFrom(src => src.CaseImages))
			.ReverseMap();
		}
	}
}
