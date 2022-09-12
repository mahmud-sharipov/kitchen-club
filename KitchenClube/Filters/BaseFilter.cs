namespace KitchenClube.Filters;

public abstract class BaseFilter : Attribute
{
    protected readonly KitchenClubContext _context;
    public BaseFilter(KitchenClubContext context)
    {
        _context = context;
    }
}
