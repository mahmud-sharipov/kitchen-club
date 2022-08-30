namespace KitchenClube.Services;

public class ServiceBace<TEntity> where TEntity : BaseEntity
{
    protected readonly KitchenClubContext _context;
    protected readonly DbSet<TEntity> _dbSet;
    protected readonly IHttpContextAccessor _contextAccessor;
    protected readonly IMapper _mapper;

    public ServiceBace(KitchenClubContext context, DbSet<TEntity> dbSet, IMapper mapper)
    {
        _context = context;
        _dbSet = dbSet;
        _contextAccessor = new HttpContextAccessor();
        _mapper = mapper;
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