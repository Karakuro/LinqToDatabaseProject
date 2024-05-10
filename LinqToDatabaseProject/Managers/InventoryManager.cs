using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class InventoryManager(GameDbContext ctx)
        : GenericManager<Inventory>(ctx), IInventoryManager
    {
        public double GetAvgInventoryDiversity(int id)
        {
            var result = (from cha in (from inv in GetAll()
                                       where inv.Character.PlayerId == id
                                       select new { inv.CharacterId, inv.ItemId }).Distinct()
                          group cha by cha.CharacterId into g
                          select g.Count()).Average();

            return result;
        }
    }
}
