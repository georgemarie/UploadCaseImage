using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.DB.Enums;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.UnitOfWorks;
using UploadingCaseImages.Service.Utilities;

namespace UploadingCaseImages.Service
{
	public class BodyImageService : IBodyImageService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public BodyImageService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<GenericResponseModel<IEnumerable<BodyImageDto>>> GetAllImageCaseAsync()
		{
			var images = await _unitOfWork.AnatomyRepository.GetAll();
			var imageDtos = images.Select(image => _mapper.Map<BodyImageDto>(image));
			return GenericResponseModel<IEnumerable<BodyImageDto>>.Success(imageDtos);
		}

		public async Task<GenericResponseModel<BodyImageDto>> GetImageByIdAsync(int id)
		{
			var image = await _unitOfWork.AnatomyRepository.GetById(id);
			if (image == null)
			{
				return GenericResponseModel<BodyImageDto>.Failure("Image not found", null);
			}

			var imageDto = _mapper.Map<BodyImageDto>(image);
			return GenericResponseModel<BodyImageDto>.Success(imageDto);
		}

		public async Task<GenericResponseModel<bool>> AddImageAsync(BodyImageDto dto)
		{
			var image = _mapper.Map<AnatomyModel>(dto);
			_unitOfWork.AnatomyRepository.Add(image);
			await _unitOfWork.SaveChangesAsync();
			return GenericResponseModel<bool>.Success(true);
		}

		public async Task<GenericResponseModel<bool>> UpdateImageAsync(int id, UpdateBodyImageDto dto)
		{
			var image = await _unitOfWork.AnatomyRepository.GetById(id);
			if (image == null)
			{
				return GenericResponseModel<bool>.Failure("Image not found", null);
			}

			image.ImagePath = dto.ImagePath;
			image.BodyPart = dto.BodyPart;
			_unitOfWork.AnatomyRepository.Update(image);
			await _unitOfWork.SaveChangesAsync();
			return GenericResponseModel<bool>.Success(true);
		}

		public async Task<GenericResponseModel<bool>> DeleteImageAsync(int id)
		{
			var image = await _unitOfWork.AnatomyRepository.GetById(id);
			if (image == null)
			{
				return GenericResponseModel<bool>.Failure("Image not found", null);
			}

			_unitOfWork.AnatomyRepository.Delete(id);
			await _unitOfWork.SaveChangesAsync();
			return GenericResponseModel<bool>.Success(true);
		}

		public async Task<GenericResponseModel<IEnumerable<BodyImageDto>>> FilterImagesByBodyPartAsync(AnatomyEnum bodyPart)
		{
			var images = await _unitOfWork.AnatomyRepository.GetData(img => img.BodyPart == bodyPart);
			var imageDtos = images.Select(image => _mapper.Map<BodyImageDto>(image));
			return GenericResponseModel<IEnumerable<BodyImageDto>>.Success(imageDtos);
		}
	}
}
