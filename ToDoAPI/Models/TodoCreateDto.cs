namespace ToDoAPI.Models
{
    public class TodoCreateDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? DueDate { get; set; }
    }
}