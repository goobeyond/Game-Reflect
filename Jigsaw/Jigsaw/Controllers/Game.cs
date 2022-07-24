using Jigsaw.Database;
using Jigsaw.Models;
using Jigsaw.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Jigsaw.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GameController : ControllerBase
    {
        private readonly ILogger<GameController> _logger;
        private readonly GameContext _context;

        public GameController(ILogger<GameController> logger, GameContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet("{id}")]
        public Game Get(long id)
        {
            return _context.Games.FirstOrDefault(x => x.Id == id);
        }

        [HttpPut("{id}")]
        public void Put([FromRoute]long id, [FromBody] Game game)
        {
            var thisGame = _context.Games.First(x => x.Id == id);
            thisGame.CurrentState = game.CurrentState;
;
            _context.SaveChanges();
        }

        [HttpPut("reset/{id}")]
        public void Rest([FromRoute] long id)
        {
            var thisGame = _context.Games.First(x => x.Id == id);
            thisGame.CurrentState = thisGame.CurrentState.OrderBy(x => Guid.NewGuid()).ToList();
            _context.SaveChanges();
        }


        //[HttpPost]
        //public void Post([FromBody] Game game)
        //{
        //    game.Id = 1;
        //    _context.Games.Add(game);
        //    _context.SaveChanges();
        //}
    }
}