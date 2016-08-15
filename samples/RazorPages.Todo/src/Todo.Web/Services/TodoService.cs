using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Logging;

namespace RazorPages.Todo.Web
{
    public class HttpTodoService : ITodoService
    {
        private HttpClient _httpClient;
        ILogger _logger;


        public HttpTodoService(ILoggerFactory loggerFactory)
        {
            _httpClient = new HttpClient();
            _logger = loggerFactory.CreateLogger<HttpTodoService>();
        }

        public async Task AddTodoItem(TodoItem item)
        {
            var itemString = JsonConvert.SerializeObject(item);
            _logger.LogCritical("ITEM TO SEND" + itemString);
            var result = await _httpClient.PutAsync("http://todo.service:5000/", new StringContent(itemString, Encoding.UTF8, "application/json"));
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            var todoString = await _httpClient.GetStringAsync("http://todo.service:5000/");
            return JsonConvert.DeserializeObject<IEnumerable<TodoItem>>(todoString);
        }
    }

    public class InMemoryTodoService : ITodoService
    {
        private List<TodoItem> _todos;

        public InMemoryTodoService ()
        {
            _todos = new List<TodoItem>();
        }

        public Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return Task.FromResult(_todos as IEnumerable<TodoItem>);
        }

        public Task AddTodoItem(TodoItem item)
        {
            _todos.Add(item);
            return Task.CompletedTask;
        }
    }
}