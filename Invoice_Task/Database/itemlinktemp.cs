namespace Invoice_Task.Database
{
    public class itemlinktemp
    {
        public int Id { get; set; }
        public int Tempid { get; set; }
        public Item? Item { get; set; }
        public int Quantity { get; set; }
        public DateTime Created { get; set; }=DateTime.Now;
    }
}
