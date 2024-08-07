using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using UploadingCaseImages.DB.Model;

namespace UploadingCaseImages.Common.Interceptors;

public class AuditableEntityInterceptor : SaveChangesInterceptor
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public AuditableEntityInterceptor(IHttpContextAccessor httpContextAccessor)
		=> _httpContextAccessor = httpContextAccessor;

	public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
				DbContextEventData eventData,
				InterceptionResult<int> result,
				CancellationToken cancellationToken = default)
	{
		var dbContext = eventData.Context;
		if (dbContext is null)
		{
			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}

		var authenticatedUser = _httpContextAccessor
			.HttpContext?
			.User?
			.Claims
			.FirstOrDefault(c => c.Type == "UserId");
		var entries = eventData.Context.ChangeTracker.Entries<AuditEntity>()
			.Where(e => e.State is EntityState.Added or EntityState.Modified);

		foreach (var entityEntry in entries)
		{
			if (entityEntry.State == EntityState.Added)
			{
				entityEntry.Property(x => x.CreatedOn).CurrentValue = DateTime.UtcNow.AddHours(2);
				entityEntry.Property(x => x.CreatedBy).CurrentValue = authenticatedUser?.Value;
			}
			else
			{
				entityEntry.Property(x => x.ModifiedOn).CurrentValue = DateTime.UtcNow.AddHours(2);
				entityEntry.Property(x => x.ModifiedBy).CurrentValue = authenticatedUser?.Value;
			}
		}

		return base.SavingChangesAsync(eventData, result, cancellationToken);
	}
}
