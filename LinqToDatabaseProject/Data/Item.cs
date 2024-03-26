namespace LinqToDatabaseProject.Data
{
    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public bool IsWeapon { get; set; }
        public int RarityId { get; set; }
        public Rarity? Rarity { get; set; }
        public List<Inventory>? Inventory { get; set; }
    }
}
