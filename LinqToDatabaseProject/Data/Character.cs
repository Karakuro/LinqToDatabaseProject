using System.ComponentModel.DataAnnotations.Schema;

namespace LinqToDatabaseProject.Data
{
    public class Character
    {
        private const int MAX_LIFE = 100;
        public int CharacterId { get; set; }
        public string Nickname { get; set; }
        public int LifePoints { get; set; }
        public int PlayerId { get; set; }
        public Player? Player { get; set; }
        public List<Inventory>? Inventory { get; set; }

        public void SetLife(int value)
        {
            LifePoints += value;
            if (LifePoints > MAX_LIFE) LifePoints = MAX_LIFE;
        }
    }
}
