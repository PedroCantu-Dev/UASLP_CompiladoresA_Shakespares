using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace proyectoCompiladoresA.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult sumar(int o1, int o2)
        {
            int res = o1 + o2;
            return Json(res);
        }

        public JsonResult operaciones(int o1, int o2)
        {
            int sumar = o1 + o2;
            int restar = o1 - o2;
            return Json(new { suma = sumar, resta = restar });
        }


        public ActionResult tablaDatos()
        {
            List<string> cadenas = new List<string> { "hola", "mundo" };
            ViewBag.nombre = "Eré";
            ViewBag.apellido = "Avalos";
            ViewBag.edad = 20;
            return PartialView(cadenas);
        }

    }
}