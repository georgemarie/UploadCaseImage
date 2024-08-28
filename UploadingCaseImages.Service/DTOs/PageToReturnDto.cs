using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UploadingCaseImages.Service.DTOs;
public class PageToReturnDto<T>
{
	public int PageNumber { get; set; }
	public int PageSize { get; set; }
	public int TotalRecords { get; set; }
	public List<T> Items { get; set; }
}
