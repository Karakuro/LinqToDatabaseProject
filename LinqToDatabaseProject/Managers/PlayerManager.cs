using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class PlayerManager(GameDbContext ctx) 
        : GenericManager<Player>(ctx), IPlayerManager
    {

        public void Test() { }
    }
}
