using System;
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
using UploadingCaseImages.DB;
using UploadingCaseImages.Service;
using AutoMapper;
using UploadingCaseImages.Service.Profiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UploadingCaseImagesContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("UploadingCaseDbContext")));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddAutoMapper(typeof(MyMapper));
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});
builder.Services.AddDbContext<UploadingCaseImagesContext>(options =>
    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICaseService, CaseService>();

builder.Services.AddDbContext<UploadingCaseImagesContext>(options =>
	options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IAnatomyService, AnatomyService>();

builder.Services.AddControllers()
	.ConfigureApiBehaviorOptions(options =>
	{
		options.InvalidModelStateResponseFactory = context => ValidationResult(context);
	})
	.AddJsonOptions(o =>
	{
		o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
		o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
	});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
	var supportedCultures = new List<CultureInfo>
	{
		new CultureInfo("en"),
		new CultureInfo("ar")
	};
	opts.DefaultRequestCulture = new RequestCulture("en");
	opts.SupportedCultures = supportedCultures;
	opts.SupportedUICultures = supportedCultures;
});

builder.Services.AddCors(opt =>
	opt.AddDefaultPolicy(o => o.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

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
}
else
{
	app.UseHsts();
}
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCookiePolicy(new CookiePolicyOptions
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
