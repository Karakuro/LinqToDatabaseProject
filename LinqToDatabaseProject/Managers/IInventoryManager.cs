using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public interface IInventoryManager : IManager<Inventory>
    {
        public double GetAvgInventoryDiversity(int id);
    }
}
