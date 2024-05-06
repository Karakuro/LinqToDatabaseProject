using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class ItemManager(GameDbContext ctx)
        : GenericManager<Item>(ctx), IItemManager
    {
    }
}
