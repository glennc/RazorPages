using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RazorPages.Todo.Service
{
    public class TodoController : ControllerBase
    {
        private static readonly List<TodoItem> _todos = new List<TodoItem>();
        ILogger _logger;

        public TodoController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TodoController>();
        }

        [HttpGet("/")]
        public IEnumerable<TodoItem> GetTodoItems()
        {
            return _todos;
        }

        [HttpPut("/")]
        public IActionResult AddTodoItem([FromBody]TodoItem item)
        {
            _todos.Add(item);
            _logger.LogInformation($"Added {item.Description}");
            return Ok();
        }
    }
}