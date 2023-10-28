using System.Runtime.Serialization.Formatters.Binary;

namespace Invoice_Task.Database
{
    [Serializable]
    public class ItemView
    {
        public int Id { get; set; } 
        public int Code { get; set; }
        public string Name { get; set; } = "";
        public int Quantity { get; set; }
        public double Price { get; set; }
        public double Total
        {
            get { return Price*Quantity; }
        }
        public ItemView() { }
        public ItemView(Item item)
        {
            Id = item.Id;
            Name=item.Name;
            Price = item.price;
            Code = item.code;
        }
     
    }
}
