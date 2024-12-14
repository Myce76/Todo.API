using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Todo.API.Data.Enums;

namespace Todo.API.Data
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }
        [Description("Short description of the todo task")]
        public string Description { get; set; } = default!;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public States Status{ get; set; } = States.NotReady;
    }
}
