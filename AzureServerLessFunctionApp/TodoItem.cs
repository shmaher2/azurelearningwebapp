namespace AzureServerLessFunctionApp
{
    public class TodoItem
    {
        public TodoItem(string description)
        {
            this.Description = description;           
        }

        public string Id { get; set; }
        public string Description { get; set; }

        public bool  IsCompleted { get; set; }
    }

    public class ToDoCreateModel
    {
        public string Description { get; set; }
    }
}