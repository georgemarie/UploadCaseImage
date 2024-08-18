using System;

public class CaseService : ICaseService
{
	private readonly UploadingCaseImagesContext _context;

	public CaseService(UploadingCaseImagesContext context)
	{
		_context = context;
	}

	public async Task SaveCaseAsync(Case caseData)
	{
		_context.Cases.Add(caseData);
		await _context.SaveChangesAsync();
	}

	public async Task<string> GetBodyPartAsync(int anatomyId)
	{
		var anatomy = await _context.AnatomyModels.FirstOrDefaultAsync(a => a.AnatomyId == anatomyId);
		return anatomy?.BodyPart.ToString();
	}
}

