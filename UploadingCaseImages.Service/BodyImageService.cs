using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.DB.Enums;
using UploadingCaseImages.Repository;
using UploadingCaseImages.Service.DTOs;

namespace UploadingCaseImages.Service
{
	public class BodyImageService : IBodyImageService
	{
		private readonly IGenericRepository<BodyImageModel> _repository;
		private readonly IMapper _mapper;

		public BodyImageService(IGenericRepository<BodyImageModel> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<IEnumerable<BodyImageDto>> GetAllImagesAsync()
		{
			var images = await _repository.GetAll();
			return images.Select(image => _mapper.Map<BodyImageDto>(image));
		}

		public async Task<BodyImageDto> GetImageByIdAsync(int id)
		{
			var image = await _repository.GetById(id);
			return _mapper.Map<BodyImageDto>(image);
		}

		public async Task AddImageAsync(UpdateBodyImageDto dto)
		{
			var image = _mapper.Map<BodyImageModel>(dto);
			_repository.Add(image);
			await _repository.SaveChangesAsync();
		}

		public async Task UpdateImageAsync(int id, UpdateBodyImageDto dto)
		{
			var image = await _repository.GetById(id);
			if (image != null)
			{
				image.ImagePath = dto.ImagePath;
				image.BodyPart = dto.BodyPart;
				_repository.Update(image);
				await _repository.SaveChangesAsync();
			}
		}

		public async Task DeleteImageAsync(int id)
		{
			var image = await _repository.GetById(id);
			if (image != null)
			{
				_repository.Delete(id);
				await _repository.SaveChangesAsync();
			}
		}

		public async Task<IEnumerable<BodyImageDto>> FilterImagesByBodyPartAsync(BodyPart bodyPart)
		{
			var images = await _repository.GetData(img => img.BodyPart == bodyPart);
			return images.Select(image => _mapper.Map<BodyImageDto>(image));
		}
	}
}
