using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Turismo.Bean;
using Turismo.Models;

namespace Turismo.Controllers
{
    public class AnuncioController : Controller
    {
        AnuncioModel model = new AnuncioModel();

        // GET: Create Anuncio
        public ActionResult Create()
        {
            return View();
        }

        // GET: List Anuncios
        public ActionResult List()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        public JsonResult Create(Paquete x, string[] itiDia, string[] itiTitulo, string[] itiDescripcion, string[] itiAlojamiento, string[] itiAlimentacion)
        {
            Usuario usuario = Session["usuario"] as Usuario;
            x.idusuario = usuario.idusuario;

            string resultado = model.Create(x, itiDia, itiTitulo, itiDescripcion, itiAlojamiento, itiAlimentacion);

            if (resultado == "Anuncio creado satisfactoriamente.") {
                moverFotos(x.foto1, x.foto2, x.foto3, x.foto4, x.foto5, x.foto6);                
            }      

            return Json(resultado);
        }

        //Void: Mover Fotos
        public void moverFotos(HttpPostedFileBase foto1, HttpPostedFileBase foto2, HttpPostedFileBase foto3, HttpPostedFileBase foto4,
            HttpPostedFileBase foto5, HttpPostedFileBase foto6) {
            string ruta = Server.MapPath("~/Img/");
            foto1.SaveAs(ruta + "\\" + foto1.FileName);
            foto2.SaveAs(ruta + "\\" + foto2.FileName);
            foto3.SaveAs(ruta + "\\" + foto3.FileName);
            foto4.SaveAs(ruta + "\\" + foto4.FileName);
            foto5.SaveAs(ruta + "\\" + foto5.FileName);
            foto6.SaveAs(ruta + "\\" + foto6.FileName);
        }

        // POST: List Destinos
        [HttpPost]
        public JsonResult LstDestinos() {
            List<Destino> destinos = model.LstDestinos();
            return Json(destinos);
        }

        // POST: List Alojamientos
        [HttpPost]
        public JsonResult LstAlojamientos()
        {
            List<Alojamiento> alojamientos = model.LstAlojamientos();
            return Json(alojamientos);
        }

        // POST: List Anuncios
        [HttpPost]
        public JsonResult LstAnuncios()
        {
            List<Paquete> anuncios = model.LstAnuncios();

            return Json(anuncios);
        }

    }
}