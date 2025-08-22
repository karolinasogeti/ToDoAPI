using Microsoft.AspNetCore.Mvc;
using ToDoAPI.Models;
using ToDoAPI.Services.TodoApi;

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpPost]
        public IActionResult Add([FromBody] TodoCreateDto dto)
        {
            try
            {
                var created = _service.Add(dto.Title, dto.Description, dto.DueDate);
                return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult MarkCompleted(int id)
        {
            _service.MarkCompleted(id);
            return NoContent();
        }

        [HttpPut("edit/{id}")]
        public IActionResult Edit(int id, [FromBody] TodoCreateDto dto)
        {
            try
            {
                _service.Edit(id, dto.Title, dto.Description, dto.DueDate);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return NoContent();
        }
    }
}
