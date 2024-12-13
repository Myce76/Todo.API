using System.ComponentModel.DataAnnotations;
using Todo.API.Data.Enums;

namespace Todo.API.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; } = default!;
        public States Status{ get; set; } = States.NotReady;
    }
}
