using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.Service.Utilities;
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
			.Include(a => a.Anatomy)
			.Include(a => a.CaseImages)
			.ToListAsync();

		var patientCasesDto = _mapper.Map<List<PatientCaseToReturnDto>>(patientCases);

		return GenericResponseModel<IEnumerable<PatientCaseToReturnDto>>.Success(patientCasesDto);
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

	public async Task<GenericResponseModel<int>> AddPatientCaseAsync(PatientCaseToSave dto)
	{
		var patientCase = _mapper.Map<PatientCase>(dto);
		_unitOfWork.Repository<PatientCase>().Add(patientCase);
		await _unitOfWork.SaveChanges();
		return GenericResponseModel<int>.Success(patientCase.Id);
	}

	public async Task<GenericResponseModel<PatientCaseToReturnDto>> GetPatientCaseByIdAsync(int id)
	{
		var patientCase = await _unitOfWork.Repository<PatientCase>()
			.FindBy(c => c.Id == id, true)
			.Include(a => a.Anatomy)
			.Include(a => a.CaseImages)
			.FirstOrDefaultAsync();
		if (patientCase is null)
			return new GenericResponseModel<PatientCaseToReturnDto>
			{
				Message = Constants.FailureMessage,
				Data = null,
				ErrorList = new List<ErrorResponseModel> { ErrorResponseModel.Create(nameof(id), "Patient case not found.") }
			};

		var patientCaseDto = _mapper.Map<PatientCaseToReturnDto>(patientCase);

		return GenericResponseModel<PatientCaseToReturnDto>.Success(patientCaseDto);
	}
}
