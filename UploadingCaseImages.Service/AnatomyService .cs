using System;

public class AnatomyService : IAnatomyService
{
	private readonly UploadingCaseImagesContext _context;

	public AnatomyService(UploadingCaseImagesContext context)
	{
		_context = context;
	}

	public async Task<AnatomyModel> GetBodyPartByNameAsync(string bodyPartName)
	{
		if (!Enum.TryParse<AnatomyEnum>(bodyPartName, true, out var bodyPartEnum))
		{
			return null; // Return null if the body part name doesn't match the enum
		}

		return await _context.AnatomyModels
			.FirstOrDefaultAsync(a => a.BodyPart == bodyPartEnum);
	}
}
