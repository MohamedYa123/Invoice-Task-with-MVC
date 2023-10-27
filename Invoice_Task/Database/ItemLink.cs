namespace Invoice_Task.Database
{
    public class ItemLink
    {
        public int Id { get; set; }
        public Invoice Invoice { get; set; }
        public Item? item { get; set; }
        public int quantity { get; set; }
    }
}
