namespace LinqToDatabaseProject.Data
{
    public class Rarity
    {
        public int RarityId { get; set; }
        public string Name { get; set; }
        public float DropRate { get; set; }
        public List<Item>? Items { get; set; }
    }
}
