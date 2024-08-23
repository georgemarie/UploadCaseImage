using FluentValidation;

namespace UploadingCaseImages.Service.DTOs.Validators;
internal sealed class PatientCaseToSaveValidator : AbstractValidator<PatientCaseToSave>
{
	public PatientCaseToSaveValidator()
	{
		RuleFor(a => a.AnatomyId).NotEmpty();
		RuleFor(a => a.PatientId).NotEmpty();
		RuleFor(a => a.CaseImages).NotEmpty();
		RuleFor(a => a.VisitDate).NotEmpty();
	}
}
