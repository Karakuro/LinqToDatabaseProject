using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class PlayerManager(GameDbContext ctx) : GenericManager<Player>(ctx), IManager<Player>
    {
    }
}
