using System.Globalization;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Scrutor;
using UploadingCaseImages.Common.Handlers;
using UploadingCaseImages.Common.Interceptors;
using UploadingCaseImages.DB;
using UploadingCaseImages.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AuditableEntityInterceptor>();

builder.Services.AddDbContext<UploadingCaseImagesContext>((sp, optionBuilder) =>
{
	optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
	var auditableInterceptor = sp.GetRequiredService<AuditableEntityInterceptor>();
	optionBuilder.AddInterceptors(auditableInterceptor);
});

// Add services to the container.
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddControllers()
			.ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = context => ValidationResult(context))
			.AddJsonOptions(o =>
			{
				o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
				o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
			});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(
opts =>
{
	List<CultureInfo> supportedCultures =
	[
		new CultureInfo("en"), new CultureInfo("ar")
	];
	opts.DefaultRequestCulture = new RequestCulture("en");
	opts.SupportedUICultures = supportedCultures;
});
builder.Services.AddCors(opt => opt.AddDefaultPolicy(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.Scan(selector => selector
				.FromAssemblies(
					UploadingCaseImages.Integrations.AssemblyReference.Assembly,
					UploadingCaseImages.Service.AssemblyReference.Assembly,
					UploadingCaseImages.Repository.AssemblyReference.Assembly,
					UploadingCaseImages.UnitOfWorks.AssemblyReference.Assembly)
				.AddClasses()
				.UsingRegistrationStrategy(RegistrationStrategy.Skip)
				.AsImplementedInterfaces()
				.WithScopedLifetime());

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssembly(UploadingCaseImages.Service.AssemblyReference.Assembly, includeInternalTypes: true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	app.UseDeveloperExceptionPage();
}
else
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions()
{
	MinimumSameSitePolicy = SameSiteMode.Strict
});

app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static BadRequestObjectResult ValidationResult(ActionContext context)
{
	var errorList = context.ModelState
							.Where(state => state.Value.ValidationState == ModelValidationState.Invalid)
							.SelectMany(
								state => state.Value.Errors,
								(state, error) => new ErrorResponseModel
								{
									PropertyName = state.Key,
									Message = error.ErrorMessage,
								})
							.ToList();

	return new BadRequestObjectResult(GenericResponseModel<bool>.Failure("Validation Error", errorList));
}
