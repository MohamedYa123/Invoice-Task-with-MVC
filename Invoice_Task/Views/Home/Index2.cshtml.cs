using Invoice_Task.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection;

namespace Invoice_Task.Views.Pages
{
    public class Index : PageModel
    {
        [BindProperty]
        public List<InvoiceView> invoiceViews { get; set; } = new List<InvoiceView>();
        public int Pagenum { get; set; }
        public int NumPerpage { get; set; } = 10;
        public Index(int pagenum)
        {
            InvoiceView v = new InvoiceView();
            invoiceViews.Add(v);
            invoiceViews.Add(v);
            invoiceViews.Add(v);
            invoiceViews.Add(v);
            invoiceViews.Add(v);
            this.Pagenum = pagenum;
        }
        public void OnGet(string id)
        {

        }
    }
}
