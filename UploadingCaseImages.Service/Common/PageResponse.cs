using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UploadingCaseImages.Service.DTOs;
using UploadingCaseImages.Service.Utilities;

namespace UploadingCaseImages.Service.Common;
public class PageResponse<T> : BaseResponseModel
{
	public PageToReturnDto<T> Data { get; set; }

	protected PageResponse(string message, IList<ErrorResponseModel> errorLists)
		: base(message, errorLists)
	{
		Type t = typeof(T);
		if (t.GetConstructor(Type.EmptyTypes) != null)
		{
			Data = new PageToReturnDto<T>();
		}
	}

	protected PageResponse(string message, int pageNumber, int pageSize, int totalRecords, List<T> items) : base(message)
	{
		Data = new PageToReturnDto<T>
		{
			PageNumber = pageNumber,
			PageSize = pageSize,
			TotalRecords = totalRecords,
			Items = items
		};
	}



	public static PageResponse<T> Failure(string message, IList<ErrorResponseModel> errorLists)
		=> new(message, errorLists);

	public static PageResponse<T> Failure(List<T> items)
		=> new(Constants.FailureMessage, 0, 0, 0, items);
	public static PageResponse<T> Success(int pageNumber, int pageSize, int totalRecords, List<T> items)
		=> new(Constants.SuccessMessage, pageNumber, pageSize, totalRecords, items);
}
