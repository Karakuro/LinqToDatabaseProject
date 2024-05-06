using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class InventoryManager(GameDbContext ctx)
        : GenericManager<Inventory>(ctx), IInventoryManager
    {
    }
}
