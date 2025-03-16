using App.Application.Contracts.Persistence;

namespace App.Persistance;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
	public Task<int> SaveChangesAsync() => context.SaveChangesAsync();
}
