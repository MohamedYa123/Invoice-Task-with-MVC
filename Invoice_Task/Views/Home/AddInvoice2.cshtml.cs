using Invoice_Task.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Invoice_Task.Views.Home
{
    public class AddInvoice
    {

        public AddInvoice()
        {
            for (int i = 0; i < 3; i++)
            {
                Customer customer = new Customer();
                customer.Id = i;
                customer.Name = $"Customer {i + 1}";
                Customers.Add(customer);
                Item item = new Item();
                item.Id = i;
                item.price = (i + 1) * 10;
                item.Name = $"Item {i + 1}";
                item.code = i;
                Items.Add(item);
            }
        }
        [BindProperty]
        //
        public int Code { get;set; }
        [BindProperty]
        public DateTime Date {  get;set; }=DateTime.Now;
        [BindProperty]
        public int Customer { get; set; }
        //
        [BindProperty]
        public List<Customer> Customers { get; set; }=new List<Customer>();
        [BindProperty]
        public List<Item> Items { get; set; }=new List<Item>();
        [BindProperty]
        public int Item { get;set; }
        [BindProperty]
        public int Quantity { get; set; } = 1;
        [BindProperty]
        public double Total2 { get; set; }
        [BindProperty]
        public List<ItemView> Addeditems { get; set; }=new List<ItemView> ();
        public double Total
        {
            get
            {
                double sum = 0;
                foreach (var item in Addeditems)
                {
                    sum += item.Total;
                }
                return sum;
            }
        }
        public void OnGet()
        {
           
        }
     
        public void OnPost(string submitbutton)
        {
            if(submitbutton == "Add Item")
            {
                var item = Items[Item];
                var itemview=new ItemView(item);
                itemview.Quantity= Quantity;
                Addeditems.Add(itemview);
            }
        }
    }
}
