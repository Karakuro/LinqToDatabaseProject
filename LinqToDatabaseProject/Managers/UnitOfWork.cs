using LinqToDatabaseProject.Data;

namespace LinqToDatabaseProject.Managers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GameDbContext _ctx;
        public IPlayerManager PlayerManager { get; private set; }
        public ICharacterManager CharacterManager { get; private set; }
        public IItemManager ItemManager { get; private set; }
        public IRarityManager RarityManager { get; private set; }
        public IInventoryManager InventoryManager { get; private set; }


        public UnitOfWork(GameDbContext ctx)
        {
            _ctx = ctx;
            PlayerManager = new PlayerManager(ctx);
            CharacterManager = new CharacterManager(ctx);
            ItemManager = new ItemManager(ctx);
            RarityManager = new RarityManager(ctx);
            InventoryManager = new InventoryManager(ctx);
        }

        public bool Commit()
        {
            return _ctx.SaveChanges() > 0;
        }
    }
}
