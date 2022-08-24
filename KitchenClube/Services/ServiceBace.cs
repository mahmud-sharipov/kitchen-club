namespace KitchenClube.Services;

public class ServiceBace<TEntity> where TEntity : BaseEntity
{
    protected readonly KitchenClubContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public ServiceBace(KitchenClubContext context, DbSet<TEntity> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    protected async Task<TEntity> FindOrThrowExceptionAsync(Guid id) =>
        await _dbSet.FindOrThrowExceptionAsync(id);
}


public static class EFExtensions
{
    public static async Task<TEntity> FindOrThrowExceptionAsync<TEntity>(this DbSet<TEntity> entities, Guid id) where TEntity : BaseEntity
    {
        var food = await entities.FindAsync(id);
        return food ?? throw new NotFoundException(typeof(TEntity).Name, id);
    }
}