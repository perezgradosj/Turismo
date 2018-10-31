using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turismo.Bean;
using Turismo.Models;

namespace Turismo.Controllers
{
    public class PaqueteController : Controller
    {
        PaqueteModel model = new PaqueteModel();

        // GET: Paquete
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult FindById(int idpaquete) {

            Paquete x = model.FindById(idpaquete);

            return Json(x);
        }

        [HttpPost]
        public JsonResult LstByRegion(string region)
        {

            List<Paquete> x = model.LstByRegion(region);

            return Json(x);
        }
    }
}