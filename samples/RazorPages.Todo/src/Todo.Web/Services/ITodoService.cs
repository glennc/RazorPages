using System.Collections.Generic;
using System.Threading.Tasks;

namespace RazorPages.Todo.Web
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task AddTodoItem(TodoItem item);
    }
}