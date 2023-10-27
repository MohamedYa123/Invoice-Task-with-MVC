using Microsoft.EntityFrameworkCore;
using Invoice_Task.Database;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Task.Database
{
    public class dbManager: DbContext
    {
       public DbSet<Customer> Customers {  get; set; }
       public DbSet<Invoice> Invoices {  get; set; }
       public DbSet<Item> Items {  get; set; }
       public DbSet<ItemLink> Itemlinks {  get; set; }
       public DbSet<itemlinktemp> itemlinktemps {  get; set; }
       protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            
            options.UseSqlServer(@"Server=MOHAMEDYASSER\SQLEXPRESS;Database=InvoiceDB;TrustServerCertificate=True;Trusted_Connection=True");
            //options.UseSqlServer(@"Data Source=SQL8005.site4now.net;Initial Catalog=db_a96cf2_medo;User Id=db_a96cf2_medo_admin;Password=Doublemedo123;TrustServerCertificate=True");//website
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
