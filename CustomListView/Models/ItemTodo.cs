using System;

namespace CustomListView.Models
{
    public class ItemTodo
    {
        public string Id { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime FinishedDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
}