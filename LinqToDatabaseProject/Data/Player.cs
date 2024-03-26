namespace LinqToDatabaseProject.Data
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string AccountName { get; set; }
        public long Credits { get; set; }
        public int AccountLevel { get; set; }
        public List<Character>? Characters { get; set; }
    }
}
