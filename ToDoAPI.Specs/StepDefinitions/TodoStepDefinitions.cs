using Reqnroll;
using System.Collections.Generic;
using System.Net.Http.Json;
using ToDoAPI.Models;
using ToDoAPI.Services.TodoApi;

namespace ToDoAPI.Specs.StepDefinitions
{
    [Binding]
    public class TodoStepDefinitions
    {
        private readonly ITodoService _todoService = new TodoService();
        private IReadOnlyList<Todo> _lastFetchedTodos;


        [Given("att todo-listan är tom")]
        public void GivenAttTodoListanArTom()
        {
            var todos = _todoService.GetAll();
            foreach (var todo in todos)
            {
                _todoService.Delete(todo.Id);
            }
        }

        [When("jag öppnar todo-appen")]
        public async Task WhenJagOppnarTodoAppen()
        {
            _lastFetchedTodos = _todoService.GetAll();
        }

        [Then("ska jag se att listan är tom")]
        public void ThenSkaJagSeAttListanArTom()
        {
            Assert.Empty(_lastFetchedTodos);
        }

        [When("jag lägger till en ny todo med titeln {string}")]
        public void WhenJagLaggerTillEnNyTodoMedTiteln(string title)
        {
            _todoService.Add(title);
        }

        [Then("ska todo-listan innehålla en todo med titeln {string}")]
        public void ThenSkaTodoListanInnehallaEnTodoMedTiteln(string title)
        {
            var todos = _todoService.GetAll();
            Assert.Contains(todos, t => t.Title == title);
        }

        [Given("att todo-listan innehåller en todo med titeln {string} som inte är klar")]
        public void GivenAttTodoListanInnehallerEnTodoMedTitelnSomInteArKlar(string title)
        {
            _todoService.Add(title);
        }

        [When("jag markerar todo:n {string} som klar")]
        public void WhenJagMarkerarTodonSomKlar(string title)
        {
            var id = _todoService.GetAll().FirstOrDefault(t => t.Title == title)?.Id ?? 0;
            _todoService.MarkCompleted(id);
        }

        [Then("ska todo:n {string} vara markerad som klar")]
        public void ThenSkaTodonVaraMarkeradSomKlar(string p0)
        {
            var todos = _todoService.GetAll();
            var todo = todos.FirstOrDefault(t => t.Title == p0);

            Assert.NotNull(todo);
            Assert.True(todo.IsCompleted, $"Todo with title '{p0}' should be marked as completed but is not.");
        }

        [Given("att todo-listan innehåller en todo med titeln {string}")]
        public void GivenAttTodoListanInnehallerEnTodoMedTiteln(string title)
        {
            _todoService.Add(title);
        }

        [When("jag tar bort todo:n {string}")]
        public void WhenJagTarBortTodon(string title)
        {
            var id = _todoService.GetAll().FirstOrDefault(t => t.Title == title)?.Id ?? 0;
            _todoService.Delete(id);
        }

        [Then("ska todo-listan vara tom")]
        public void ThenSkaTodoListanVaraTom()
        {
            var todos = _todoService.GetAll();
            Assert.Empty(todos);
        }

        [When("jag lägger till följande todos")]
        public void WhenJagLaggerTillFoljandeTodos(DataTable table)
        {
            foreach (var row in table.Rows)
            {
                var title = row["titel"];
                _todoService.Add(title);
            }
        }

        [Then("ska todo-listan innehålla {int} todos")]
        public void ThenSkaTodo_ListanInnehallaTodos(int antal)
        {
            var todos = _todoService.GetAll();
            Assert.Equal(antal, todos.Count);
        }

    }
}
