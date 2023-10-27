using Invoice_Task.Database;
using Invoice_Task.SiteClasses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Invoice_Task
{
    public class AddInvoiceModel 
    {
        public void OnGet(int Id,string ActionType,int pagenum)
        {
            if (ActionType == "Delete")
            {
                //  ModelState.AddModelError("model.Username", "Invalid Username");
                var db = new dbManager();
                var Invoice=db.Invoices.FirstOrDefault(i=>i.Id == Id);
                if (Invoice != null)
                {
                    db.Invoices.Remove(Invoice);
                    db.SaveChanges();
                    //return Redirect("index?pagenum="+pagenum);
                    redirect = "index?pagenum=" + pagenum;
                }
            }
            else if(ActionType =="Edit")
            {
                ActionTypeP = ActionType;
                InvoicetoEdit = Id;
                var db = new dbManager();
                var Invoice = db.Invoices.Include(i=>i.Customer).First(i => i.Id == InvoicetoEdit);
                var itemlinks = db.Itemlinks.Include(i=>i.item).Where(i => i.Invoice == Invoice).ToList();
                foreach(var a in itemlinks)
                {
                    itemlinktemp itemlinktemp=new itemlinktemp();
                    Item item = db.Items.First(i => i.Id == a.item.Id);
                    itemlinktemp.Item = item;
                    itemlinktemp.Quantity=a.quantity;
                    itemlinktemp.Tempid = idrand;
                    db.itemlinktemps.Add(itemlinktemp);
                }
                Customer = Invoice.Customer.Id;
                Code = Invoice.Code;
                db.SaveChanges();
                load();
            }
            else 
            {
                ActionTypeP =  "Add";
            }
         //   return Page();
        }
        void load()
        {
            
            var db = new dbManager();
            Items =db.Items.ToList();
            //
            Customers=db.Customers.ToList();
            Addeditems.Clear();
            var itemstemplink = db.itemlinktemps.Include(i=>i.Item).Where(i=>i.Tempid== idrand).ToList();
            foreach(var itemtemp in itemstemplink)
            {
                ItemView itemview = new ItemView();
                itemview.Id=itemtemp.Item.Id;
                itemview.Quantity = itemtemp.Quantity;
                itemview.Price = itemtemp.Item.price;
                itemview.Name = itemtemp.Item.Name;
                Addeditems.Add(itemview);
            }
            //
        }
        public AddInvoiceModel()
        {
            load();
           
        }
        [BindProperty]
        public int InvoicetoEdit { get; set; }
        [BindProperty]
        public string ActionTypeP { get;set; }
        [BindProperty]
        public int idrand { get; 
            set; } = Manager.getrandomid();
        [BindProperty]
        //
        public int Code { get; set; }
        [BindProperty]
        public DateTime Date { get; set; } = DateTime.Now;
        [BindProperty]
        public int Customer { get; set; }
        //
        [BindProperty]
        public List<Customer> Customers { get; set; } = new List<Customer>();
        [BindProperty]
        public List<Item> Items { get; set; } = new List<Item>();
        [BindProperty]
        public int Item { get; set; }
        [BindProperty]
        public int Quantity { get; set; } = 1;
        [BindProperty]
        public double Total2 { get; set; }
        [BindProperty]
        public List<ItemView> Addeditems { get; set; } = new List<ItemView>();
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
        public string redirect { get; set; } = "";
        public void OnPost(string submitbutton)
        {
          
            load();
            if (submitbutton == "Add Item")
            {
                var db = new dbManager();
                var item = Items[Item];
                var itemview = new ItemView(item);
                itemview.Quantity = Quantity;
                //save itemlinktemp
                itemlinktemp itemlinktemp = new itemlinktemp();
                itemlinktemp.Tempid = idrand;
                itemlinktemp.Quantity = Quantity;
                 item = db.Items.First(i => i.Id == item.Id);
                itemlinktemp.Item =item;
                db.itemlinktemps.Add(itemlinktemp);
                db.SaveChanges();
                //end
                Addeditems.Add(itemview);
                //return Page();
            }
            else if(ActionTypeP!="Edit")
            {
                var db=new dbManager();
              
                var temps=db.itemlinktemps.Where(i=>i.Id == idrand).ToList();
                db.itemlinktemps.RemoveRange(temps);
                db.SaveChanges();
                var customer=db.Customers.First(i=>i.Id == Customer);
                Invoice invoice = new Invoice();
                invoice.Customer = customer;
                invoice.Amount = Total;// Addeditems.Count;
                invoice.Code= Code;
                invoice.Date = Date;
                db.Invoices.Add(invoice);
                db.SaveChanges();
                invoice=db.Invoices.First(i=>i.Id==invoice.Id);
                var itemlinks = db.Itemlinks.Include(i => i.item).Where(i => i.Invoice == invoice).ToList();
                db.Itemlinks.RemoveRange(itemlinks);
                foreach (var itemview in Addeditems)
                {
                    ItemLink itemLink = new ItemLink();
                    itemLink.quantity = itemview.Quantity;
                    itemLink.item =db.Items.First(i=>i.Id== itemview.Id);
                    itemLink.Invoice = invoice;
                    db.Itemlinks.Add(itemLink);
                }
                db.SaveChanges();
                redirect = "index";
            //    return Redirect("index");
            }
            else
            {
                var db = new dbManager();
                var temps = db.itemlinktemps.Where(i => i.Id == idrand).ToList();
                db.itemlinktemps.RemoveRange(temps);
                db.SaveChanges();
                var invoice = db.Invoices.Include(i=>i.Customer).First(i => i.Id==InvoicetoEdit);
                var customer = db.Customers.First(i => i.Id == Customer);
                invoice.Customer = customer;
                invoice.Amount = Total;// Addeditems.Count;
                invoice.Code = Code;
                invoice.Date = Date;
                db.Invoices.Update(invoice);
                db.SaveChanges();
                invoice = db.Invoices.Include(i => i.Customer).First(i => i.Id == InvoicetoEdit);
                var itemlinks=db.Itemlinks.Where(i=>i.Invoice==invoice).ToList();
                db.Itemlinks.RemoveRange(itemlinks);
                db.SaveChanges();
                foreach (var itemview in Addeditems)
                {
                    ItemLink itemLink = new ItemLink();
                    itemLink.quantity = itemview.Quantity;
                    itemLink.item = db.Items.First(i => i.Id == itemview.Id);
                    itemLink.Invoice = invoice;
                    db.Itemlinks.Add(itemLink);
                }
                db.SaveChanges();
                redirect = "index";
                //return Redirect("index");
            }
        }
    }
}
