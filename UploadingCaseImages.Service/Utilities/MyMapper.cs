using AutoMapper;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Service.Profiles
{
	public class MyMapper : Profile
	{
		public MyMapper()
		{
			CreateMap<UpdateBodyImageDto, AnatomyModel>();
			CreateMap<AnatomyModel, BodyImageDto>();
			CreateMap<BodyImageDto, AnatomyModel>();
		}
	}
}
