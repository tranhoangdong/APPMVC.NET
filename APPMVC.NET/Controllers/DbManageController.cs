using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APPMVC.NET.Controllers
{
    public class DbManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
