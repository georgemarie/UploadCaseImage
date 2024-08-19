using System;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Services
{
	public interface ICaseService
	{
		Task<Case> GetCaseByIdAsync(int id);
	}
}
