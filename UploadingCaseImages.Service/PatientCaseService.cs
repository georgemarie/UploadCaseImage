using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.DB;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.UnitOfWorks;

namespace UploadingCaseImages.Service;
public class PatientCaseService : IPatientCaseService
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;
	private readonly UploadingCaseImagesContext _context;

	public PatientCaseService(IUnitOfWork unitOfWork, IMapper mapper, UploadingCaseImagesContext context)
	{
		_unitOfWork = unitOfWork;
		_mapper = mapper;
		_context = context;
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

	public async Task<GenericResponseModel<bool>> AddPatientCaseAsync(PatientCaseToSave dto)
	{
		var patientCase = _mapper.Map<PatientCase>(dto);
		_unitOfWork.Repository<PatientCase>().Add(patientCase);
		await _unitOfWork.SaveChanges();
		foreach (var caseImageDto in dto.CaseImages)
		{
			var caseImage = _mapper.Map<CaseImage>(caseImageDto);
			caseImage.PatientCaseId = patientCase.PatientCaseId;
			_unitOfWork.Repository<CaseImage>().Add(caseImage);
		}
		await _unitOfWork.SaveChanges();
		return GenericResponseModel<bool>.Success(true);
	}
	public async Task<PatientCaseToReturnDto> GetCaseByIdAsync(int id)
	{
		var patientCase = await _context.Set<PatientCase>()
			.Include(c => c.CaseImages)
			.Include(c => c.Anatomy)
			.FirstOrDefaultAsync(c => c.PatientCaseId == id);

		if (patientCase == null)
		{
			return null;
		}
		return _mapper.Map<PatientCaseToReturnDto>(patientCase);
	}
}
