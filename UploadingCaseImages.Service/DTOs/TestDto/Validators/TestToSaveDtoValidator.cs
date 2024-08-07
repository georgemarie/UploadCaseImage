using FluentValidation;

namespace UploadingCaseImages.Service.DTOs.TestDto.Validators;
internal sealed class TestToSaveDtoValidator : AbstractValidator<TestToSaveDto>
{
	public TestToSaveDtoValidator()
	{
		RuleFor(x => x.Name)
			.NotEmpty();
	}
}
