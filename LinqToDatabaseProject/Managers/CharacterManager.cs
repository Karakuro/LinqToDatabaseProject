using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class CharacterManager(GameDbContext ctx) 
        : GenericManager<Character>(ctx), ICharacterManager
    {
    }
}
