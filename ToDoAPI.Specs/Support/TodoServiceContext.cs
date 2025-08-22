using ToDoAPI.Services.TodoApi;

namespace ToDoAPI.Specs.Support
{
    public class TodoServiceContext
    {
        public ITodoService TodoService { get; } = new TodoService();
        public Exception? LastException { get; set; } 
    }
}