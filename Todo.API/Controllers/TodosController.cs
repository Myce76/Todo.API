using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.API.Models;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        // In Memory Storage for simplicity
        private static readonly List<TodoItem> _todoItems = [];

        // GET api/todos
        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> Get()
        {
            return Ok(_todoItems);
        }

        // GET api/todos/1
        [HttpGet("{id}")]
        public ActionResult<TodoItem> Get(int id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id);
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        // POST api/todos
        [HttpPost]
        public ActionResult Create([FromBody] TodoItem item) 
        {
            _todoItems.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            
            var itemToUpdate = _todoItems.FirstOrDefault(x => x.Id == item.Id);
            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Description = item.Description;
            itemToUpdate.Status = item.Status;

            return NoContent();
        }

        // DELETE api/todos/1
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var itemToDelete = _todoItems.FirstOrDefault(x => x.Id == id);
            if (itemToDelete == null)
            {
                return NotFound();
            }

            _todoItems.Remove(itemToDelete);

            return NoContent();
        }
    }
}
