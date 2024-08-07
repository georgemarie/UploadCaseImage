using System;

namespace UploadingCaseImages.DB.Contracts
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
        DateTime? DeletedOn { get; set; }
    }
}
