namespace ToDoAPI.Services
{
    using System.Collections.Generic;
    using ToDoAPI.Models;

    namespace TodoApi
    {
        public interface ITodoService
        {
            IReadOnlyList<Todo> GetAll();
            Todo Add(string title);
            void MarkCompleted(int id);
            void Delete(int id);
        }

        public class TodoService : ITodoService
        {
            private readonly List<Todo> _todos = new();
            private int _nextId = 1;

            public IReadOnlyList<Todo> GetAll() => _todos;

            public Todo Add(string title)
            {
                var todo = new Todo { Id = _nextId++, Title = title, IsCompleted = false };
                _todos.Add(todo);
                return todo;
            }

            public void MarkCompleted(int id)
            {
                var todo = _todos.Find(t => t.Id == id);
                if (todo != null) todo.IsCompleted = true;
            }

            public void Delete(int id)
            {
                var todo = _todos.Find(t => t.Id == id);
                if (todo != null) _todos.Remove(todo);
            }
        }
    }

}
