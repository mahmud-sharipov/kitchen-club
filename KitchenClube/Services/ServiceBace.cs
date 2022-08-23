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

    protected async Task<TEntity> FindAsync(Guid id)
    {
        var food = await _dbSet.FindAsync(id);
        //TODO: Review and remove
        //if (food == null)
        //    throw new NotFoundException(nameof(Food), id);

        return food ?? throw new NotFoundException(nameof(Food), id);
    }
}
