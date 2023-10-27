namespace Invoice_Task.Database
{
    public class Invoice
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public Customer? Customer { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }=DateTime.Now;
    }
}
