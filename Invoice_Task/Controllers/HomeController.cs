using Invoice_Task.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Invoice_Task.Views.Home;
namespace Invoice_Task.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int PageNum)
        {
            if (PageNum == 0)
            {
               // PageNum = 1;
            }
            if (PageNum <=0)
            {
                return Redirect("/home/index?PageNum=" + 1);
            }
            var model = new Invoice_Task.Index();
            model.OnGet(PageNum);
            return View(model);
        }

        public IActionResult Privacy(string id)
        {
           return View();
        }
        public IActionResult Delete()
        {
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddInvoice(int Id, string ActionType, int pagenum)
        {
            var model = new Invoice_Task.AddInvoiceModel();
            model.OnGet(Id, ActionType, pagenum);
            if (model.redirect != "")
            {
                return Redirect(model.redirect);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult AddInvoice(string submitbutton, AddInvoiceModel model)
        {
            model.OnPost(submitbutton);
            if (model.redirect != "")
            {
                return Redirect(model.redirect);
            }
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}