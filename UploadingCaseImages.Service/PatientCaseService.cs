using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.Common;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.Service.Utilities;
using UploadingCaseImages.UnitOfWorks;

namespace UploadingCaseImages.Service;
public class PatientCaseService : IPatientCaseService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly IHttpContextAccessor _contextAccessor;

	public PatientCaseService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		this._contextAccessor = contextAccessor;
	}

	public async Task<PageResponse<PatientCaseToReturnDto>> GetPatientCaseAsync(GetPatientCaseDto dto)
	{
		var query = _unitOfWork
			.Repository<PatientCase>()
			.FindBy(a => true);

		query = ApplyFiltrationOnPatientCases(dto, query);

		var totalRecords = await query.CountAsync();

		var patientCases = await query
			.Include(a => a.Anatomy)
			.Include(a => a.CaseImages)
			.OrderByDescending(x => x.CreatedAt)
			.Skip((dto.PageNumber - 1) * dto.PageSize)
			.Take(dto.PageSize)
			.ToListAsync();

		var patientCasesDto = _mapper.Map<List<PatientCaseToReturnDto>>(patientCases);

		return PageResponse<PatientCaseToReturnDto>.Success(dto.PageNumber, dto.PageSize, totalRecords, patientCasesDto);
	}

	private static IQueryable<PatientCase> ApplyFiltrationOnPatientCases(GetPatientCaseDto dto, IQueryable<PatientCase> query)
	{
		if (dto.VisitDate.HasValue && dto.VisitDate.Value != default)
		{
			query = query.Where(p => p.VisitDate.Date.Year == dto.VisitDate.Value.Date.Year
			&& p.VisitDate.Date.Month == dto.VisitDate.Value.Date.Month
			&& p.VisitDate.Date.Day == dto.VisitDate.Value.Date.Day);
		}

		if (!string.IsNullOrEmpty(dto.AnatomyId))
		{
			var anatomyIds = dto.AnatomyId.Split(',').Select(int.Parse).ToList();
			query = query.Where(p => anatomyIds.Contains(p.AnatomyId));
		}

		return query;
	}

	public int? GetUserIdFromToken()
	{
		var idClaim = _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId");
		return idClaim != null ? int.Parse(idClaim.Value) : null;
	}

	public string GetUserTypeFromToken()
	{
		return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserType")?.Value;
	}
	public async Task<GenericResponseModel<int>> AddPatientCaseAsync(PatientCaseToSave dto)
	{
		var userId = GetUserIdFromToken();
		var userType = GetUserTypeFromToken();

		if (userId is null || string.IsNullOrEmpty(userType) || userType != "Doctor")
		{
			return new GenericResponseModel<int>
			{
				Data = 0,
				Message = Constants.FailureMessage,
				ErrorList = new List<ErrorResponseModel> { new ErrorResponseModel { Message = "Not Authorized", PropertyName = "Doctor" } }
			};
		}

		var patientCase = _mapper.Map<PatientCase>(dto);
		patientCase.DoctorId = userId.Value;
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
