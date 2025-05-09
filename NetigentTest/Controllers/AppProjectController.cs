﻿using Microsoft.AspNetCore.Mvc;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;
using NetigentTest.Models.ViewModels;
using NetigentTest.Services;

namespace NetigentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppProjectController : ControllerBase
    {
        private readonly IAppProjectService _appProjectService;

        public AppProjectController(IAppProjectService appProjectService)
        {
            _appProjectService = appProjectService;
        }

        [HttpPost]
        public async Task<ActionResult<AppProject>> Create([FromBody] CreateEditAppProjectBindingModel model)
        {
            var appProject = await _appProjectService.CreateAsync(model);
            return Ok(appProject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<AppProject>> Edit(int id, [FromBody] CreateEditAppProjectBindingModel model)
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
        public async Task<ActionResult<AppProjectIndividualViewModel>> GetOne(int id)
        {
            var appProject = await _appProjectService.GetAsync(id);
            if (appProject == null) return NotFound();
            return Ok(appProject);
        }

        [HttpGet]
        public async Task<ActionResult<List<AppProjectSearchViewModel>>> GetAll()
        {
            var appProjects = await _appProjectService.GetAsync();
            return Ok(appProjects);
        }
    }
}
