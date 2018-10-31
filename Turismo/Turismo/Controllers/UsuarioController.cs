using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turismo.Bean;
using Turismo.Models;

namespace Turismo.Controllers
{
    public class UsuarioController : Controller
    {
        UsuarioModel model = new UsuarioModel();

        [HttpPost]
        public JsonResult Create(Usuario usuario)
        {
            bool existencia = model.FindUserByCorreo(usuario.correo);

            if (existencia)
                return Json(existencia);
            else {
                string resultado = model.Create(usuario);
                return Json(resultado);
            }            
        }

        [HttpPost]
        public JsonResult Autentication(Usuario usuario)
        {
            Usuario x = model.Autentication(usuario);

            if (x != null)
            {
                Session["usuario"] = new Usuario();
                Session["usuario"] = x;

                return Json(x);
            }
            else {
                return Json(false);
            }           
        }

        [HttpGet]
        public ActionResult Logout() {
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}