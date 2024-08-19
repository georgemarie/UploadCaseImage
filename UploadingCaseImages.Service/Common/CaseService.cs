using UploadingCaseImages.Repository;
using UploadingCaseImages.DB.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
public class CaseService
{
	private readonly IGenericRepository<Case> _caseRepository;
	public CaseService(IGenericRepository<Case> caseRepository)
	{
		_caseRepository = caseRepository;
	}
	public async Task<Case> GetCaseByIdAsync(int id)
    {
		return await _caseRepository
				   .GetQueryable()
				   .Include(c => c.ImageUrls)
				   .FirstOrDefaultAsync(c => c.Id == id);
	}
}
