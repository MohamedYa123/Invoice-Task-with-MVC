using Invoice_Task.Database;

namespace Invoice_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var db = new dbManager();
            var customerscount=db.Customers.Count();
            if (customerscount == 0)
            {
                for(int i=0;i<5; i++)
                {
                    Customer customer = new Customer();
                    customer.Name = $"Customer {i+1}";
                    db.Customers.Add(customer);
                }
            }
            var itemscount=db.Items.Count();
            if (itemscount == 0)
            {
                for(int i=0;i<5; i++)
                {
                    Item item = new Item();
                    item.Name = $"Item {i+1}";
                    item.price = (i + 1) * 10;
                    item.code = i;
                    db.Items.Add(item); 
                }
            }
            db.SaveChanges();
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.Run();
        }
    }
}