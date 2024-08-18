using System;

public interface ICaseService
{
	Task SaveCaseAsync(Case caseData);
	Task<string> GetBodyPartAsync(int anatomyId);
}

