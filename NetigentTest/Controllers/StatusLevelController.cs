using Microsoft.AspNetCore.Mvc;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;
using NetigentTest.Services;

namespace NetigentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusLevelController : ControllerBase
    {
        private readonly IStatusLevelService _statusLevelService;

        public StatusLevelController(IStatusLevelService statusLevelService)
        {
            _statusLevelService = statusLevelService;
        }

        [HttpPost]
        public async Task<ActionResult<StatusLevel>> Create([FromBody] CreateStatusLevelBindingModel model)
        {
            var statusLevel = await _statusLevelService.CreateAsync(model);
            return CreatedAtAction(nameof(GetOne), new { id = statusLevel.Id }, statusLevel);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StatusLevel>> Edit(int id, [FromBody] EditStatusLevelBindingModel model)
        {
            model.Id = id;
            var statusLevel = await _statusLevelService.EditAsync(model);
            if (statusLevel == null) return NotFound();
            return Ok(statusLevel);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _statusLevelService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusLevel>> GetOne(int id)
        {
            var statusLevel = await _statusLevelService.GetAsync(id);
            if (statusLevel == null) return NotFound();
            return Ok(statusLevel);
        }

        [HttpGet]
        public async Task<ActionResult<List<StatusLevel>>> GetAll()
        {
            var statusLevels = await _statusLevelService.GetAsync();
            return Ok(statusLevels);
        }
    }
}
