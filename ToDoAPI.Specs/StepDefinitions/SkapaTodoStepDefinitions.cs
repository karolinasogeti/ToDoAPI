using ToDoAPI.Models;
using ToDoAPI.Services.TodoApi;
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

        [When("jag lägger till en ny todo med titeln {string}, beskrivningen {string} och förfallodatumet {string}")]
        public void WhenJagLaggerTillEnNyTodoMedTitelnBeskrivningenOchForfallodatumet(string titel, string beskrivning, string datum)
        {
            _context.LastException = null;
            try
            {
                _context.TodoService.Add(titel, beskrivning, datum);
            }
            catch (Exception ex)
            {
                _context.LastException = ex;
            }
        }

        [Then("ska todo-listan innehålla en todo med titeln {string}, beskrivningen {string} och förfallodatumet {string}")]
        public void ThenSkaTodoListanInnehallaEnTodoMedTitelnBeskrivningenOchForfallodatumet(string titel, string beskrivning, string datum)
        {
            var todos = _context.TodoService.GetAll();

            DateTime? expectedDate = null;
            if (!string.IsNullOrWhiteSpace(datum))
            {
                if (DateTime.TryParse(datum, out var parsedDate))
                    expectedDate = parsedDate;
            }

            Assert.Contains(todos, t =>
                t.Title == titel &&
                t.Description == beskrivning &&
                t.DueDate == expectedDate
            );
        }

        [When("jag lägger till en ny todo med")]
        public void WhenJagLaggerTillEnNyTodoMed(DataTable dataTable)
        {
            foreach (var row in dataTable.Rows)
            {
                var titel = row["titel"];
                var beskrivning = row["beskrivning"];
                var datum = row["förfallodatum"];
                _context.TodoService.Add(titel, beskrivning, datum);
            }
        }

        [Then("ska todo-listan innehålla en todo med")]
        public void ThenSkaTodo_ListanInnehallaEnTodoMed(DataTable dataTable)
        {
            var todos = _context.TodoService.GetAll();

            foreach (var row in dataTable.Rows)
            {
                var titel = row["titel"].Trim('"');
                var beskrivning = row["beskrivning"].Trim('"');
                var datum = row["förfallodatum"].Trim('"');

                DateTime? expectedDate = null;
                if (!string.IsNullOrWhiteSpace(datum))
                {
                    if (DateTime.TryParse(datum, out var parsedDate))
                        expectedDate = parsedDate;
                }

                Assert.Contains(todos, t =>
                    t.Title == titel &&
                    t.Description == beskrivning &&
                    t.DueDate == expectedDate
                );
            }
        }

        [Then("ska jag få felmeddelandet {string}")]
        public void ThenSkaJagFaFelmeddelandet(string felmeddelande)
        {
            Assert.NotNull(_context.LastException);
            Assert.Contains(felmeddelande, _context.LastException.Message);
        }
    }
}
