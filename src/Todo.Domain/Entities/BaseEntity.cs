using System.ComponentModel.DataAnnotations;

namespace Todo.Domain.Entities;
public abstract class BaseEntity
{
    [Key]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
