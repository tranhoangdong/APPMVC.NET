using APPMVC.NET.Service;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APPMVC.NET.Controllers
{
    public class FirstController : Controller
    {
        private readonly ILogger<FirstController> _logger;

        private readonly ProductService _productService;
        public FirstController(ILogger<FirstController> logger, ProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }
        public string Index()
        {
            _logger.LogWarning("thongbao");
            _logger.LogInformation("Index Action");
            return "toi la Index cua First";
        }
        public void Nothing()
        {
            _logger.LogInformation("Nothing Action");
            Response.Headers.Add("Hi", "xin chao cac ban");
        }
        public object Anything() => DateTime.Now;
        public IActionResult Readme()
        {
            var content = @"
            xin chao cac ban


            hiiii
            ";
            return Content(content, "text/html");
        }
        public IActionResult Bird()
        {
            // Startup.ContentRootPath;
            string filePath = Path.Combine(Startup.ContentRootPath, "Files", "Bird.jpg");
            var bytes = System.IO.File.ReadAllBytes(filePath);

            return File(bytes, "image/jpg"); 
        }
        public IActionResult IphonePrice()
        {
            return Json(
                new
                {
                    ProdcutName = "Iphonex",
                    Price = 1000
                }
            ) ;
           
        }
        public IActionResult Privacy()
        {
            var url = Url.Action("Privacy", "Home"); // chuyen huong den Home/Privacy
            return LocalRedirect(url); // local  host 
        }
        public IActionResult Google()
        {
            var url = "https://Google.com";
            return Redirect(url); 
        }
        public IActionResult HelloView(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                userName = "khach";
            // View() -> Razor engine doc .cshtml , templete
            //----------------------------------------------
            // View(Templete)- đường dẫn tuyệt đối đến .cshtml
            //View(Templete,model)
            //  return View("/MyView/Xinchao1.cshtml",userName);


            //Xinchao2.cshtml -> /View/First/Xinchao2.cshtml
            //return View("Xinchao2", userName);

            //HelloView.cshtml ->/View/First/HelloView.cshtml
            return View((object)userName) ;
        }
        public IActionResult ViewProduct(int? id )
        {
            var product = _productService.Where(p => p.Id ==id).FirstOrDefault();
            if (product == null)
            {
                TempData["StatusMessage"] = "san pham da het";
                return LocalRedirect(Url.Action("Index", "Home"));
            }
            //return NotFound();
            //return View(product);
            //this.ViewData["product"] = product;
            //return View("Viewproduct2");

            ViewBag.product = product;
            return View("Viewproduct3");
        }

       
    }
}
