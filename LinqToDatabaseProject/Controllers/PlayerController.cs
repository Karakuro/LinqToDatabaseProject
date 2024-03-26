using LinqToDatabaseProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LinqToDatabaseProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly GameDbContext _ctx;

        public PlayerController(GameDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IQueryable<Player> result = (from p in _ctx.Players
                                   select p);
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult Get(int id)
        {
            var result = (from p in _ctx.Players
                          where p.PlayerId == id
                          select p).SingleOrDefault();
            return Ok(result);
        }

        [HttpGet]
        [Route("GetDetails")]
        public IActionResult GetPlayers(List<int> ids)
        {
            var result = (from p in _ctx.Players
                          join id in ids
                          on p.PlayerId equals id
                          select p).ToList();
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}/GetFullInventory")]
        public IActionResult GetFullInventory(int id)
        {
            //restituire la lista completa di tutti gli item posseduti da tutti i character di un player
            //creando come risultante un oggetto che abbia le seguenti caratteristiche: Nome Item e quantità totale
            var result = (from item in _ctx.Items
                          join inv in _ctx.Inventories on item.ItemId equals inv.ItemId
                          join cha in _ctx.Characters on inv.CharacterId equals cha.CharacterId
                          where cha.PlayerId == id
                          group inv.ItemCount by item into grouped
                          select new { grouped.Key.Name, Total = grouped.Sum() });

            var result3 = _ctx.Items
                .Join(_ctx.Inventories, item => item.ItemId, inv => inv.ItemId, (item, inv) => new { item, inv })
                .Join(_ctx.Characters, invItem => invItem.inv.CharacterId, cha => cha.CharacterId, (invItem, cha) => new { invItem.inv, invItem.item, cha })
                .Where(x => x.cha.PlayerId == id)
                .GroupBy(x => x.item)
                .Select(x => new { x.Key.Name, Total = x.Sum(y => y.inv.ItemCount) }).ToList();

            var result2 = (from inv in _ctx.Inventories
                          where inv.Character.PlayerId == id
                          group inv by inv.Item into g
                          select new { g.Key.Name, Total = g.Sum(y => y.ItemCount) });

            var result4 = _ctx.Inventories
                .Where(x => x.Character.PlayerId == id)
                .GroupBy(x => x.Item)
                .Select(x => new { x.Key.Name, Total = x.Sum(y => y.ItemCount) }).ToList();

            return Ok(result);
        }
    }
}
