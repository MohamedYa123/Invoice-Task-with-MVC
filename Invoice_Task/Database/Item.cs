namespace Invoice_Task.Database
{
    public class Item
    {
        public int Id { get; set; }
        public int code { get; set; }
        public string Name { get; set; } = "";
        public double price { get; set; }
    }
}
