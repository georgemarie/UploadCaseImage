using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.UnitOfWorks;

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

	public async Task<GenericResponseModel<IEnumerable<PatientCaseToReturnDto>>> GetPatientCaseAsync(GetPatientCaseDto dto)
	{
		var query = _unitOfWork
			.Repository<PatientCase>()
			.FindBy(a => true);

		query = ApplyFiltrationOnPatientCases(dto, query);

		var patientCases = await query
			.Include(a => a.CaseImages)
			.Select(p => new PatientCaseToReturnDto
			{
				PatientCaseId = p.PatientCaseId,
				Note = p.Note,
				VisitDate = p.VisitDate,
				AnatomyId = p.AnatomyId,
				AnatomyName = p.Anatomy.AnatomyName,
				CreatedAt = p.CreatedAt,
				CaseImages = p.CaseImages.Select(a => new CaseImageToReturnDto
				{
					CaseImageId = a.CaseImageId,
					CaseName = a.CaseName,
					CreatedAt = a.CreatedAt,
					CasePath = a.CasePath
				}).ToList()
			})
			.ToListAsync();

		return GenericResponseModel<IEnumerable<PatientCaseToReturnDto>>.Success(patientCases);
	}

	private static IQueryable<PatientCase> ApplyFiltrationOnPatientCases(GetPatientCaseDto dto, IQueryable<PatientCase> query)
	{
		if (dto.VisitDate.HasValue && dto.VisitDate.Value != default)
		{
			query = query.Where(p => p.VisitDate.Date.Year == dto.VisitDate.Value.Date.Year
			&& p.VisitDate.Date.Month == dto.VisitDate.Value.Date.Month
			&& p.VisitDate.Date.Day == dto.VisitDate.Value.Date.Day);
		}

		if (dto.AnatomyId.HasValue && dto.AnatomyId.Value > 0)
		{
			query = query.Where(p => p.AnatomyId == dto.AnatomyId.Value);
		}

		return query;
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
