using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Diagnostics;

namespace App.Repositories.Interceptors
{
	public class AuditDbContextInterceptor : SaveChangesInterceptor
	{
		private static Dictionary<EntityState,Action<DbContext,IAuditEntity>> Behaviors = new()
		{
			{EntityState.Added, AddBehavior},
			{EntityState.Modified,ModifiedBehavior}
		};

		private static void AddBehavior(DbContext context,IAuditEntity auditEntity)
		{
			auditEntity.Created = DateTime.Now;
			context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
		}

		private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
		{
			context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
			auditEntity.Updated = DateTime.Now;
		}
		public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
			InterceptionResult<int> result, CancellationToken cancellationToken = new CancellationToken())
		{

			foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
			{

				if (entityEntry.Entity is not IAuditEntity auditEntity) continue;


				Behaviors[entityEntry.State](eventData.Context, auditEntity);

			}

			return base.SavingChangesAsync(eventData, result, cancellationToken);
		}
	}
}
