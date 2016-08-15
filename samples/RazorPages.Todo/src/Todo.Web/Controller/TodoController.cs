
// using System.Collections.Generic;
// using Microsoft.AspNetCore.Mvc;

// namespace RazorPages.Todo.Web
// {
//     public class TodoController: Controller
//     {
//         private ITodoService _service;

//         public TodoController (ITodoService service)
//         {
//             _service = service;
//         }

//         [HttpGet("/")]
//         public IEnumerable<TodoItem> GetTodoItems()
//         {
//             return _service.GetTodoItems();
//         }

//         [HttpPut("/")]
//         public IActionResult AddTodoItem([FromBody]TodoItem item)
//         {
//             _service.AddTodoItem(item);
//             return Ok();
//         }
//     }
// }