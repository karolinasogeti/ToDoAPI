using ToDoAPI.Models;
using ToDoAPI.Specs.Support;

namespace ToDoAPI.Specs.StepDefinitions
{
    [Binding]
    public class SkapaTodoStepDefinitions
    {
        private readonly TodoServiceContext _context;
        private IReadOnlyList<Todo> _lastFetchedTodos;

        public SkapaTodoStepDefinitions(TodoServiceContext context)
        {
            _context = context;
        }

        [Given("att todo-listan är tom")]
        public void GivenAttTodoListanArTom()
        {
            var todos = _context.TodoService.GetAll();
            foreach (var todo in todos)
            {
                _context.TodoService.Delete(todo.Id);
            }
        }

        [When("jag öppnar todo-appen")]
        public void WhenJagOppnarTodoAppen()
        {
            _lastFetchedTodos = _context.TodoService.GetAll();
        }

        [Then("ska jag se att listan är tom")]
        public void ThenSkaJagSeAttListanArTom()
        {
            Assert.Empty(_lastFetchedTodos);
        }

        [When("jag lägger till en ny todo med titeln {string}")]
        public void WhenJagLaggerTillEnNyTodoMedTiteln(string title)
        {
            _context.TodoService.Add(title, null, null);
        }

        [Then("ska todo-listan innehålla en todo med titeln {string}")]
        public void ThenSkaTodoListanInnehallaEnTodoMedTiteln(string title)
        {
            var todos = _context.TodoService.GetAll();
            Assert.Contains(todos, t => t.Title == title);
        }

        [When("jag lägger till följande todos")]
        public void WhenJagLaggerTillFoljandeTodos(DataTable table)
        {
            foreach (var row in table.Rows)
            {
                var title = row["titel"];
                _context.TodoService.Add(title, null, null);
            }
        }

        [Then("ska todo-listan innehålla {int} todos")]
        public void ThenSkaTodo_ListanInnehallaTodos(int antal)
        {
            var todos = _context.TodoService.GetAll();
            Assert.Equal(antal, todos.Count);
        }
    }
}
