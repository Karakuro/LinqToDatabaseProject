using LinqToDatabaseProject.Data;
using LinqToDatabaseProject.Managers;
using LinqToDatabaseProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace LinqToDatabaseProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        //private readonly GameDbContext _ctx;
        //private readonly IPlayerManager _manager;
        private readonly IUnitOfWork _managers;

        /*
         * Implementare le seguenti GET con LINQ to query:
            - Uso medio dell'inventario, ovvero per ogni Player, indicare in media quanti tipi 
            di oggetti sono in inventario
            - Assegnare un livello ai Character e fare Ranking dei Player per livello massimo 
            del Player e dei loro Character, riportando quindi la classifica ordinata 
            (con numero di posizione) dei giocatori con il relativo personaggio di 
            livello più alto tra quelli disponibili. In caso di pareggio, si dovrà 
            assegnare lo stesso numero di posizione ai giocatori e passare alla posizione 
            libera successiva con quelli posizionati dopo in classifica; esempio:
            1) Player: Pippo, PlayerLevel: 10, Character: Gruk, CharLevel 100
            1) Player: Pluto, PlayerLevel: 10, Character: Slash, CharLevel 100
            3) Player: Paperino, PlayerLevel: 10, Character: Jhon, CharLevel 99
            3) Player: Topolino, PlayerLevel: 10, Character: Jack, CharLevel 99
            5) Player: Gastone, PlayerLevel: 9, Character: Jason, CharLevel 100
         */
        public PlayerController(IUnitOfWork managers)
        {
            _managers = managers;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<Player> result = (from p in _managers.PlayerManager.GetAll()
                                   select p);
            return Ok(result);
        }

        [Authorize(Roles = UserRoles.Admin)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = (from p in _managers.PlayerManager.GetAll()
                          where p.PlayerId == id
                          select p).SingleOrDefault();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetails")]
        public IActionResult GetPlayers(List<int> ids)
        {
            var result = (from p in _managers.PlayerManager.GetAll()
                          join id in ids
                          on p.PlayerId equals id
                          select p).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/GetFullInventory")]
        public IActionResult GetFullInventory(int id)
        {
            var chars = _managers.CharacterManager.GetAll();
            var items = _managers.ItemManager.GetAll();
            var inventories = _managers.InventoryManager.GetAll();
            //restituire la lista completa di tutti gli item posseduti da tutti i character di un player
            //creando come risultante un oggetto che abbia le seguenti caratteristiche: Nome Item e quantità totale
            var result = (from item in items
                          join inv in inventories on item.ItemId equals inv.ItemId
                          join cha in chars on inv.CharacterId equals cha.CharacterId
                          where cha.PlayerId == id
                          group inv.ItemCount by item into grouped
                          select new { grouped.Key.Name, Total = grouped.Sum() });

            var result3 = items
                .Join(inventories, item => item.ItemId, inv => inv.ItemId, (item, inv) => new { item, inv })
                .Join(chars, invItem => invItem.inv.CharacterId, cha => cha.CharacterId, (invItem, cha) => new { invItem.inv, invItem.item, cha })
                .Where(x => x.cha.PlayerId == id)
                .GroupBy(x => x.item)
                .Select(x => new { x.Key.Name, Total = x.Sum(y => y.inv.ItemCount) }).ToList();

            var result2 = (from inv in inventories
                           where inv.Character.PlayerId == id
                          group inv by inv.Item into g
                          select new { g.Key.Name, Total = g.Sum(y => y.ItemCount) });

            var result4 = inventories
                .Where(x => x.Character.PlayerId == id)
                .GroupBy(x => x.Item)
                .Select(x => new { x.Key.Name, Total = x.Sum(y => y.ItemCount) }).ToList();

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/InventoryDiversity")]
        public IActionResult GetAvgInventoryDiversity(int id)
        {
            var result = (from cha in (from inv in _managers.InventoryManager.GetAll()
                          where inv.Character.PlayerId == id
                          select new { inv.CharacterId, inv.ItemId }).Distinct()
                          group cha by cha.CharacterId into g
                          select g.Count()).Average();

            var result2 = _managers.InventoryManager.GetAll()
                .Where(inv => inv.Character.PlayerId == id)
                .DistinctBy(inv => new { inv.CharacterId, inv.ItemId })
                .GroupBy(inv => inv.CharacterId)
                .Average(group => group.Count());

            return Ok();
        }
        /*
         * Implementare le seguenti GET con LINQ to query:
            - Uso medio dell'inventario, ovvero per ogni Player, indicare in media quanti tipi 
            di oggetti sono in inventario
            - Assegnare un livello ai Character e fare Ranking dei Player per livello massimo 
            del Player e dei loro Character, riportando quindi la classifica ordinata 
            (con numero di posizione) dei top 100 giocatori con il relativo personaggio di 
            livello più alto tra quelli disponibili. In caso di pareggio, si dovrà 
            assegnare lo stesso numero di posizione ai giocatori e passare alla posizione 
     i   i+1    libera successiva con quelli posizionati dopo in classifica; esempio:
     0   1    1) Player: Pippo, PlayerLevel: 10, Character: Gruk, CharLevel 100
     1   1    1) Player: Pluto, PlayerLevel: 10, Character: Slash, CharLevel 100
     2   3    3) Player: Paperino, PlayerLevel: 10, Character: Jhon, CharLevel 99
     3   3    3) Player: Topolino, PlayerLevel: 10, Character: Jack, CharLevel 99
     4   5    5) Player: Gastone, PlayerLevel: 9, Character: Jason, CharLevel 100
         */
        [HttpGet]
        [Route("Ranking")]
        public IActionResult GetRanking()
        {
            var result = (from p in (from p in _managers.PlayerManager.GetAll()
                                     select new { 
                                  p.PlayerId,
                                  p.AccountName,
                                  p.AccountLevel,
                                  MaxChar = p.Characters
                                        .OrderByDescending(c => c.CharacterLevel)
                                        .FirstOrDefault()
                              })
                          orderby 
                            p.AccountLevel, 
                            p.MaxChar.CharacterLevel, 
                            p.AccountName, 
                            p.MaxChar.Nickname 
                            descending
                          select new RankingModel { 
                              PlayerId = p.PlayerId,
                              PlayerName = p.AccountName,
                              PlayerLevel = p.AccountLevel,
                              CharacterId = p.MaxChar.CharacterId,
                              CharacterLevel = p.MaxChar.CharacterLevel,
                              CharacterName = p.MaxChar.Nickname
                          }).Take(100).ToList();
            int position = 1;
            result[0].Ranking = position;
            for (int i = 1; i < result.Count; i++)
            {
                if (result[i].PlayerLevel != result[i-1].PlayerLevel || 
                    result[i].CharacterLevel != result[i-1].CharacterLevel)
                    position = i + 1;

                result[i].Ranking = position;
            }
            return Ok(result);
        }
    }
}
