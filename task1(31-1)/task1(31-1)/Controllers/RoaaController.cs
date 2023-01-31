using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace task1_31_1_.Controllers
{
    public class RoaaController : Controller
    {
        // GET: Roaa
        public ActionResult Index()
        {
            return View();
        }
        public string name()
        {
            return " <h1>Roaa<h1/>";
        }
        public int Age()
        {
            return 24;
        }
        public string nationalty()
        {
            return " <h1>Jordanian<h1/>";
        }
        public string Image()
        {
            return " <img src='../img/self pic.png'> ";

        }
        //public void download()
        //{

        //    var imgPath = Server.MapPath("../img/self pic.png");
        //    Response.AddHeader("Content-Disposition", "attachment;filename=DealerAdTemplate.png");
        //    Response.WriteFile(imgPath);
        //    Response.End();
        //}
    }
}