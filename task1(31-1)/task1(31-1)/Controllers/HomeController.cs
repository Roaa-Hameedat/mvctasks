using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task1_31_1_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public string ShowImage()
        {
            return "<a href='download' > <img src='../img/cinema1.jfif'> </a>";

        }
        public void download() {

            var imgPath = Server.MapPath("../img/cinema1.jfif");
            Response.AddHeader("Content-Disposition", "attachment;filename=DealerAdTemplate.png");
            Response.WriteFile(imgPath);
            Response.End();
        }
    }
}