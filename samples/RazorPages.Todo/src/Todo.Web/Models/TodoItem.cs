
using System.ComponentModel.DataAnnotations;

namespace RazorPages.Todo.Web
{
    public class TodoItem
    {
        public int Id { get; set; }

        [StringLength(255, MinimumLength = 2)]
        public string Description { get; set; }
    }
}
