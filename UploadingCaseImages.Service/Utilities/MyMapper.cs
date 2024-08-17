using AutoMapper;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Service.Profiles
{
	public class MyMapper : Profile
	{
		public MyMapper()
		{
			CreateMap<UpdateBodyImageDto, BodyImageModel>();
			CreateMap<BodyImageModel, BodyImageDto>(); // Mapping from BodyImageModel to BodyImageDto
			CreateMap<BodyImageDto, BodyImageModel>();
		}
	}
}
