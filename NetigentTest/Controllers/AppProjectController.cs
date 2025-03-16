using Microsoft.AspNetCore.Mvc;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;
using NetigentTest.Services;

namespace NetigentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppProjectController : ControllerBase
    {
        private readonly AppProjectService _appProjectService;

        public AppProjectController(AppProjectService appProjectService)
        {
            _appProjectService = appProjectService;
        }

        [HttpPost]
        public async Task<ActionResult<AppProject>> Create([FromBody] CreateAppProjectBindingModel model)
        {
            var appProject = await _appProjectService.CreateAsync(model);
            return CreatedAtAction(nameof(GetOne), new { id = appProject.Id }, appProject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppProject>> Edit(int id, [FromBody] EditAppProjectBindingModel model)
        {
            model.Id = id;
            var appProject = await _appProjectService.EditAsync(model);
            if (appProject == null) return NotFound();
            return Ok(appProject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _appProjectService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AppProject>> GetOne(int id)
        {
            var appProject = await _appProjectService.GetOneAsync(id);
            if (appProject == null) return NotFound();
            return Ok(appProject);
        }

        [HttpGet]
        public async Task<ActionResult<List<AppProject>>> GetAll()
        {
            var appProjects = await _appProjectService.GetAllAsync();
            return Ok(appProjects);
        }
    }
}
