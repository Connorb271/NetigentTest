using Microsoft.AspNetCore.Mvc;
using NetigentTest.Models.BindingModels;
using NetigentTest.Models.DBModels;
using NetigentTest.Services;

namespace NetigentTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InquiryController : ControllerBase
    {
        private readonly InquiryService _inquiryService;

        public InquiryController(InquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        [HttpPost]
        public async Task<ActionResult<Inquiry>> Create([FromBody] CreateInquiryBindingModel model)
        {
            var inquiry = await _inquiryService.CreateAsync(model);
            return CreatedAtAction(nameof(GetOne), new { id = inquiry.Id }, inquiry);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Inquiry>> Edit(int id, [FromBody] EditInquiryBindingModel model)
        {
            model.Id = id;
            var inquiry = await _inquiryService.EditAsync(model);
            if (inquiry == null) return NotFound();
            return Ok(inquiry);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _inquiryService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Inquiry>> GetOne(int id)
        {
            var inquiry = await _inquiryService.GetOneAsync(id);
            if (inquiry == null) return NotFound();
            return Ok(inquiry);
        }

        [HttpGet]
        public async Task<ActionResult<List<Inquiry>>> GetAll()
        {
            var inquiries = await _inquiryService.GetAllAsync();
            return Ok(inquiries);
        }
    }
}
