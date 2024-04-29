namespace LinqToDatabaseProject.Models
{
    public class RankingModel
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int PlayerLevel { get; set; }
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int CharacterLevel { get; set; }
        public int Ranking { get; set; } = 0;
    }
}
