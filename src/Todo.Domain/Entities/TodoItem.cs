namespace Todo.Domain.Entities;

public class TodoItem : BaseEntity
{
    public string Description { get; set; } = default!;
    public ItemStatus Status { get; set; } = ItemStatus.NotReady;
}
