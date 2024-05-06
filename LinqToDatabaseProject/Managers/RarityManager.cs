using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class RarityManager(GameDbContext ctx)
        : GenericManager<Rarity>(ctx), IRarityManager
    {
    }
}
