namespace ToDoAPI.Services
{
    using System.Collections.Generic;
    using ToDoAPI.Models;

    namespace TodoApi
    {
        public interface ITodoService
        {
            IReadOnlyList<Todo> GetAll();
            Todo Add(string title, string? description, string? dueDate);
            void MarkCompleted(int id);
            void Delete(int id);
            void Edit(int id, string title, string? description, string? dueDate);
        }

        public class TodoService : ITodoService
        {
            private readonly List<Todo> _todos = new();
            private int _nextId = 1;

            public IReadOnlyList<Todo> GetAll() => _todos;

            public Todo Add(string title, string? description, string? dueDate)
            {
                DateTime? parsedDate = null;
                if (!string.IsNullOrWhiteSpace(dueDate))
                {
                    if (DateTime.TryParse(dueDate, out var tempDate))
                            parsedDate = tempDate;
                    else
                        throw new ArgumentException("Datumformatet är ogiltigt");
                }

                var todo = new Todo
                {
                    Id = _nextId++,
                    Title = title,
                    IsCompleted = false,
                    Description = description,
                    DueDate = parsedDate
                };

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

            public void Edit(int id, string title, string? description, string? dueDate)
            {
                var todo = _todos.Find(t => t.Id == id);
                if (todo == null)
                    throw new KeyNotFoundException("Todo med angivet id hittades inte.");

                if (string.IsNullOrWhiteSpace(title))
                    throw new ArgumentException("Titeln kan inte vara tom");

                DateTime? parsedDate = null;
                if (!string.IsNullOrWhiteSpace(dueDate))
                {
                    if (DateTime.TryParse(dueDate, out var tempDate))
                            parsedDate = tempDate;
                    else
                        throw new ArgumentException("Datumformatet är ogiltigt");
                }

                todo.Title = title;
                todo.Description = description;
                todo.DueDate = parsedDate;
            }
        }
    }

}
