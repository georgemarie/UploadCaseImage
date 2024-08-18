using System;

public interface IAnatomyService
{
	Task<AnatomyModel> GetBodyPartByNameAsync(string bodyPartName);
}
