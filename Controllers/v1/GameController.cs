using Microsoft.AspNetCore.Mvc;
using MoviesDIOApi.Model;
using MoviesDIOApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesDIOApi.Controllers.v1
{
    [ApiController]
    [Route("v1/[Controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;
        public GameController(IGameService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<List<Game>>> GetAllAsync([FromQuery] Pagination pagination)
        {
            var result = await _service.GetAllAsync(pagination);
            if (result.Count == 0)
            {
                return NoContent();
            }
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<ActionResult<Game>> GetGameByIdAsync(Guid id)
        {
            var game = await _service.GetGameByIdAsync(id);
            if (game == null)
            {
                return NoContent();
            }
            return Ok(game);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<ActionResult> DeleteGameByIdAsync([FromRoute] Guid id)
        {
            var deleteOk = await _service.DeleteGameByIdAsync(id);
            if (deleteOk)
            {
                return Ok();
            } else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult<Game>> AddGameAsync([FromBody] GameModelInput gameInput)
        {
            if (ModelState.IsValid)
            {
                var game = await _service.AddGameAsync(gameInput);
                if (game != null)
                {
                    return Ok(game);
                }
                else
                {
                    return BadRequest(new { Message = "Game already exist."});
                };
            } else
            {
                return BadRequest(new { Message = "Invalid data" });
            }                   
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<ActionResult<Game>> UpdateGameAsync([FromRoute] Guid id,[FromBody] GameModelInput gameInput)
        {

            var game = await _service.UpdateGameAsync(id, gameInput);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);            
        }
        [HttpPatch]
        [Route("{id:Guid}/price/{price}")]
        public async Task<ActionResult<Game>> UpdateValueGameAsync([FromRoute] Guid id, [FromRoute] double price)
        {
            var game = await _service.UpdateValueGameAsync(id, price);
            if (game == null)
            {
                return NotFound();
            }
            return Ok(game);
        }
    }
}
