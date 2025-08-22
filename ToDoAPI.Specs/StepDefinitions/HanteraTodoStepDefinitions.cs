using ToDoAPI.Specs.Support;

namespace ToDoAPI.Specs.StepDefinitions
{
    [Binding]
    public class HanteraTodoStepDefinitions
    {
        private readonly TodoServiceContext _context;

        public HanteraTodoStepDefinitions(TodoServiceContext context)
        {
            _context = context;
        }

        [Given("att todo-listan innehåller en todo med titeln {string} som inte är klar")]
        public void GivenAttTodoListanInnehallerEnTodoMedTitelnSomInteArKlar(string titel)
        {
            _context.TodoService.Add(titel, null, null);
        }

        [When("jag markerar todo:n {string} som klar")]
        public void WhenJagMarkerarTodonSomKlar(string titel)
        {
            var id = _context.TodoService.GetAll().FirstOrDefault(t => t.Title == titel)?.Id ?? 0;
            _context.TodoService.MarkCompleted(id);
        }

        [Then("ska todo:n {string} vara markerad som klar")]
        public void ThenSkaTodonVaraMarkeradSomKlar(string titel)
        {
            var todos = _context.TodoService.GetAll();
            var todo = todos.FirstOrDefault(t => t.Title == titel);

            Assert.NotNull(todo);
            Assert.True(todo.IsCompleted, $"Todo with title '{titel}' should be marked as completed but is not.");
        }

        [Given("att todo-listan innehåller en todo med titeln {string}")]
        public void GivenAttTodoListanInnehallerEnTodoMedTiteln(string titel)
        {
            _context.TodoService.Add(titel, null, null);
        }

        [When("jag tar bort todo:n {string}")]
        public void WhenJagTarBortTodon(string titel)
        {
            var id = _context.TodoService.GetAll().FirstOrDefault(t => t.Title == titel)?.Id ?? 0;
            _context.TodoService.Delete(id);
        }

        [Then("ska todo-listan vara tom")]
        public void ThenSkaTodoListanVaraTom()
        {
            var todos = _context.TodoService.GetAll();
            Assert.Empty(todos);
        }

        [When("jag redigerar todo:n {string} och ändrar titeln till {string}")]
        public void WhenJagRedigerarTodonOchAndrarTitelnTill(string originalTitel, string nyTitel)
        {
            var id = _context.TodoService.GetAll().FirstOrDefault(t => t.Title == originalTitel)?.Id ?? 0;
            _context.TodoService.Edit(id, nyTitel, null, null);
        }

        [When("jag försöker redigera todo:n {string} och ändrar alla egenskaper")]
        public void WhenJagForsokerRedigeraTodonOchAndrarAllaEgenskaper(string originalTitel, DataTable table)
        {
            var todos = _context.TodoService.GetAll();
            var todo = todos.FirstOrDefault(t => t.Title == originalTitel);
            if (todo == null)
                throw new Exception($"Todo med titel '{originalTitel}' hittades inte.");

            foreach (var row in table.Rows)
            {
                var titel = row["titel"].Trim('"');
                var beskrivning = row["beskrivning"].Trim('"');
                var datum = row["förfallodatum"].Trim('"');
                _context.TodoService.Edit(todo.Id, titel, beskrivning, datum);
            }
        }

        [When("jag försöker redigera todo:n {string} och anger en tom titel")]
        public void WhenJagForsokerRedigeraTodonOchAngerEnTomTitel(string originalTitel)
        {
            var todos = _context.TodoService.GetAll();
            var todo = todos.FirstOrDefault(t => t.Title == originalTitel);
            
            try
            {
                _context.TodoService.Edit(todo?.Id ?? 0, string.Empty, null, null);
            }
            catch (Exception ex)
            {
                _context.LastException = ex;
            }
        }

        [When("jag försöker redigera todo:n {string} och anger ett datumformat {string}")]
        public void WhenJagForsokerRedigeraTodoNOchAngerEttDatumformat(string titel, string datum)
        {
            var todos = _context.TodoService.GetAll();
            var todo = todos.FirstOrDefault(t => t.Title == titel);

            try
            {
                _context.TodoService.Edit(todo?.Id ?? 0, titel, null, dueDate: datum);
            }
            catch (Exception ex)
            {
                _context.LastException = ex;
            }
        }
    }
}
