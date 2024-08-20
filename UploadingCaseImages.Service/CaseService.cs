using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.DB;
using Microsoft.EntityFrameworkCore;
using UploadingCaseImages.Service.DTOs;
using AutoMapper;
using UploadingCaseImages.Service.Profiles;

namespace UploadingCaseImages.Service;
public class CaseService
{
	private readonly UploadingCaseImagesContext _context;
	private readonly IMapper _mapper;
	public CaseService(UploadingCaseImagesContext context, IMapper mapper)
	{
		_context = context;
		_mapper = mapper;
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
		if (_mapper == null)
		{
			throw new InvalidOperationException("Mapper is not initialized.");
		}
		Console.WriteLine($"PatientCase: {patientCase}"); // Check if patientCase is null
		Console.WriteLine($"Mapper: {_mapper}");
		return _mapper.Map<PatientCaseToReturnDto>(patientCase);
	}
}
