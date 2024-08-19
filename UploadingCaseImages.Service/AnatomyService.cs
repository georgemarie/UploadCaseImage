using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.UnitOfWorks;

namespace UploadingCaseImages.Service;
public class AnatomyService : IAnatomyService
{
	private readonly IUnitOfWork _unitOfWork;

	public AnatomyService(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<GenericResponseModel<List<Anatomy>>> GetAllAnatomiesAsync()
	{
		var anatomies = await _unitOfWork.Repository<Anatomy>().GetAll();
		return GenericResponseModel<List<Anatomy>>.Success(anatomies.ToList());
	}
}
