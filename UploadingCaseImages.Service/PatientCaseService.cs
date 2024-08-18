using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.UnitOfWorks;
using UploadingCaseImages.Service.Utilities;
using AutoMapper;
using UploadingCaseImages.Service.DTOs;
using System.Linq.Expressions;
using UploadingCaseImages.DB.Model;
using Microsoft.EntityFrameworkCore;


namespace UploadingCaseImages.Service;
public class PatientCaseService : IPatientCaseService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public PatientCaseService(IUnitOfWork unitOfWork, IMapper mapper)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
	}

	public async Task<GenericResponseModel<IEnumerable<GetPatientCaseDto>>> GetPatientCaseAsync(GetPatientCaseDto dto)
	{
		IQueryable<GetPatientCaseDto> query = (IQueryable<GetPatientCaseDto>)_unitOfWork.Repository<GetPatientCaseDto>().GetQueryableData(_ => true);

		// Filter by Date if provided
		if (dto.VisitDate != null)
		{
			query = query.Where(img => img.VisitDate == dto.VisitDate);
		}

		// Filter by ID if provided
		if (dto.PatientCaseId != null)
		{
			query = query.Where(img => img.PatientCaseId == dto.PatientCaseId);
		}

		// Execute the query
		var patientCases = await query.ToListAsync();

		// Map to DTOs if necessary
		var patientCaseDtos = patientCases.Select(patientCase => _mapper.Map<GetPatientCaseDto>(patientCase));

		return GenericResponseModel<IEnumerable<GetPatientCaseDto>>.Success(patientCaseDtos);
	}



	public Task<GenericResponseModel<bool>> AddPatientCaseAsync(GetPatientCaseDto dto)
	{
		//var image = _mapper.Map<Anatomy>(dto);
		//_unitOfWork.AnatomyRepository.Add(image);
		//await _unitOfWork.SaveChangesAsync();
		//return GenericResponseModel<bool>.Success(true);
		return null;
	}
}
