namespace Homework_09.Model
{
    public class BotButton
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public int ParentId { get; set; }
        public string ButtonName { get; set; }
        public string Content { get; set; }
    }
}
