using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Invoice_Task.Database;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Task
{
    public class Index 
    {
        #region Methods
        /// <summary>
        /// used to load invoice from the database to fill in the list
        /// </summary>
        void loadInvoices()
        {
            if (Pagenum > LastPagenum)
            {
                Pagenum = LastPagenum;
            }
            if (Pagenum <= 0)
            {
                Pagenum = 1;
            }
            var db = new dbManager();
            var list = db.Invoices.Include(i => i.Customer).Skip((Pagenum - 1) * NumPerpage).Take(NumPerpage).ToList();
            foreach (var a in list)
            {
                InvoiceView invoiceView = new InvoiceView();
                invoiceView.code = a.Code;
                invoiceView.Id = a.Id;
                invoiceView.Customername = a.Customer.Name;
                invoiceView.Amount = a.Amount;
                invoiceView.Date = a.Date;
                invoiceViews.Add(invoiceView);
            }
        }
        public void OnGet(int PageNum)
        {
            var db = new dbManager();
            var c = db.Invoices.Count();
            LastPagenum = c / NumPerpage + 1;
            if (c % NumPerpage == 0)
            {
                LastPagenum--;
            }
            if (PageNum > 0 &&PageNum<=LastPagenum)
            {
                Pagenum = Convert.ToInt32(PageNum);
            }
            
            loadInvoices();
           // return Page();
        }
        #endregion
        #region Properites
        // Invoice to view list
        [BindProperty]
        public List<InvoiceView> invoiceViews { get; set; } = new List<InvoiceView>();
        public int Pagenum { get; set; } = 1;
        public int LastPagenum { get; set; } = 1;
        public string Pageorpages{
            get
            {
                if (LastPagenum > 1)
                {
                    return "pages";
                }
                return "page";
            }
            }
        public int PreviousPagenum
        {
            get
            {
                if (Pagenum > 1)
                {
                    return Pagenum-1;
                }
                return 1;
            }
        }
        public int NextPagenum
        {
            get
            {
                if (invoiceViews.Count >=NumPerpage)
                {
                    return Pagenum + 1;
                }
                return Pagenum;
            }
        }
        public int NumPerpage { get; set; } = 10;
        #endregion

    }
}

