using Todo.API.Enums;

namespace Todo.API.Models
{
    public class TodoItem
    {
        public int Id { get; set; } = default(int);
        public string Description { get; set; } = default!;
        public States Status{ get; set; } = States.NotReady;
    }
}
