using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationProtection.Controllers
{
    public class ErrorController : Controller
    {
        //
        // GET: /Error/

        public ViewResult Index(String id)
        {
            ViewBag.id = id;
            return View(id);
        }

        public ViewResult NotFound()
        {
            Response.StatusCode = 404;  //you may want to set this to 200
            return View("NotFound");
        }

 
    }
}
