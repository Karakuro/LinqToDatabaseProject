namespace LinqToDatabaseProject.Managers
{
    public interface IUnitOfWork
    {
        public IPlayerManager PlayerManager { get; }
        public ICharacterManager CharacterManager { get; }
        public IItemManager ItemManager { get; }
        public IRarityManager RarityManager { get; }
        public IInventoryManager InventoryManager { get; }
        public bool Commit();
    }
}
