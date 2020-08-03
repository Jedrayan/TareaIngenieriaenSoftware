using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FurmularioDonaciones.Models;
using FurmularioDonaciones.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FurmularioDonaciones.Controllers
{
    public class FormularioDonacionController : Controller
    {
        private DB_A49FE7_BaseDonacionesContext _context;
        // GET: Formulario_Donacion_/Details/5
        public ActionResult ConsultaFormularioDonacion()

        {
            var formulario = _context.FormularioDonacion.FirstOrDefault();
            return View(formulario);
        }


        // GET: FormularioDonacionCreate/5
        public ActionResult FormularioDonacionCreate()
        {
            //foreach (var objetoModelSelectTemporal in listaMarcas)
            //{
            //    ModelSelect objetoSelect = new ModelSelect();
            //    objetoSelect.value = objetoModelSelectTemporal.IdMarca;
            //    objetoSelect.text = objetoModelSelectTemporal.NombreMarca;

            //    listaSelects.Add(objetoSelect);
            //}
            cargarMarcasSelect();
            return View();

        }


        [HttpPost]

        public ActionResult FormularioDonacionCreate(FormularioDonacion objetoFormularioDonacion, IFormFile Foto)
        {
            //recuperar un objeto de una variable de sesion, convirtiendo Json 
            var objetoUsuarioSesion = HttpContext.Session.GetString("UsuarioSesion");
            var objetoUsuarioFinal = JsonConvert.DeserializeObject<Usuario>(objetoUsuarioSesion);
            
            objetoFormularioDonacion.FechaAcopioDonacion = DateTime.Now;
            objetoFormularioDonacion.IdUsuario = objetoUsuarioFinal.IdUsuario;

            //objetoFormularioDonacion.IdMarca = 1;
            /* combertir una imagen en bits*/
            try
            {

                objetoFormularioDonacion.Tipoimagen = Foto.ContentType;

                var memoryStream = new MemoryStream();
                Foto.CopyTo(memoryStream);
                objetoFormularioDonacion.Foto = memoryStream.ToArray();


            }
            catch
            {
                Console.Write("No funca lguardar la foto  ");
            }


            bool bandera = GuardarFormulario(objetoFormularioDonacion);
            if (bandera == true)
            {
                TempData["MensajeIngreso"] = "Guardado";
                ModelState.Clear();
            }
            cargarMarcasSelect();
            return View();

         

        }


        // Contexto DB
        public FormularioDonacionController(DB_A49FE7_BaseDonacionesContext context)
        {
            _context = context;
        }


        // Cargar Marcas Select
        public void cargarMarcasSelect()
        {
            IEnumerable<Marcas> listaMarcas = _context.Marcas;

            List<ModelSelect> listaSelects = new List<ModelSelect>();

            var listaFinalSelects = listaMarcas.Select(x => new ModelSelect
            {
                value = x.IdMarca,
                text = x.NombreMarca
            }).ToList();
            ViewBag.ListaMarcasVista = listaFinalSelects;
        }
        // Funcion conbertir imagenes a bit

        //Funcion guardar nuevo Formulario de donaciones 
        public bool GuardarFormulario(FormularioDonacion objetoFormularioDonacion)
        {

            try
            {
                _context.FormularioDonacion.Add(objetoFormularioDonacion);
                _context.SaveChanges();

                return true;
            }
            catch (Exception exx)
            {
                return false;

            }

        }
    }
}