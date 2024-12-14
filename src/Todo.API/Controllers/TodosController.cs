using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Todo.API.Filters;
using Todo.Domain.Entities;

namespace Todo.API.Controllers
{
    [Route("odata/[controller]")]
    [ApiController]
    public class TodosController : ODataController
    {
        // In Memory Storage for simplicity
        private static readonly List<TodoItem> _todoItems = [];

        // GET odata/todos
        [HttpGet]
        [ODataOperationFilterCustom]
        public async Task<ActionResult<IQueryable<TodoItem>>> Get([FromODataUri] string? description, [FromODataUri] ItemStatus? states, [FromServices] ODataQueryOptions<TodoItem> options)
        {
            IQueryable results = options.ApplyTo(_todoItems.AsQueryable());

            return Ok(results as IQueryable<TodoItem>);
        }

        // GET odata/todos/1
        [HttpGet("({id})")]
        public async Task<ActionResult<TodoItem>> Get([FromRoute] Guid id)
        {
            var todoItem = _todoItems.FirstOrDefault(x => x.Id == id.ToString());
            if (todoItem == null)
            {
                return NotFound();
            }
            return Ok(todoItem);
        }

        // POST api/todos
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] TodoItem item) 
        {
            _todoItems.Add(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);
        }

        // PUT api/todos/1
        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]Guid id, [FromBody] TodoItem item)
        {
            if (id.ToString() != item.Id)
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
        public async Task<ActionResult> Delete(Guid id)
        {
            var itemToDelete = _todoItems.FirstOrDefault(x => x.Id == id.ToString());
            if (itemToDelete == null)
            {
                return NotFound();
            }

            _todoItems.Remove(itemToDelete);

            return NoContent();
        }
    }
}
