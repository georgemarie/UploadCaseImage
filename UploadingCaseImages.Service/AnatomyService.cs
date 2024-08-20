using AutoMapper;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.UnitOfWorks;

namespace UploadingCaseImages.Service;
public class AnatomyService : IAnatomyService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public AnatomyService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		this._mapper = mapper;
	}

	public async Task<GenericResponseModel<List<AnatomyDto>>> GetAllAnatomiesAsync()
	{
		var anatomies = await _unitOfWork.Repository<Anatomy>().GetAll();
		var anatomiesDto = _mapper.Map<List<AnatomyDto>>(anatomies);
		return GenericResponseModel<List<AnatomyDto>>.Success(anatomiesDto);
	}
}
