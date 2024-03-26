namespace LinqToDatabaseProject.Data
{
    public class Inventory
    {
        public int InventoryId { get; set; }
        public int ItemId { get; set; }
        public int CharacterId { get; set; }
        public int ItemCount { get; set; }
        public bool? IsPrimaryWeapon { get; set; }
        public Character? Character { get; set; }
        public Item? Item { get; set; }
    }
}
