using System.Globalization;
using System.Text;
using System.Text.Json;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Scrutor;
using UploadingCaseImages.Common.Configurations;
using UploadingCaseImages.Common.Handlers;
using UploadingCaseImages.DB;
using UploadingCaseImages.DB.Model;
using UploadingCaseImages.Repository;
using UploadingCaseImages.Service;
using UploadingCaseImages.Service.Profiles;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<UploadingCaseImagesContext>((sp, optionBuilder) =>
{
	optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"));
});

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
builder.Services.AddSwaggerGen(swagger =>
{
	swagger.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "UploadingCaseImages",
		Description = "UploadingCaseImages",
		Version = "1.0"
	});

	swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "bearer",
	});

	swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = "Bearer"
				}
			},
			Array.Empty<string>()
		}
	});
});
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

builder.Services.AddAuthentication(options =>
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
	};
});

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
app.UseSwagger();
app.UseSwaggerUI();
app.UseHsts();
app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
	MinimumSameSitePolicy = SameSiteMode.Strict
});

StaticFilesConfiguration.ConfigureStaticFiles(app);

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
