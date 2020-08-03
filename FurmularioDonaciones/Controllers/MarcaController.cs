using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FurmularioDonaciones.Models;
using Microsoft.AspNetCore.Mvc;

namespace FurmularioDonaciones.Controllers
{
    public class MarcaController : Controller
    {
        private DB_A49FE7_BaseDonacionesContext _context;

        //GET: Crear Marca
        public IActionResult CrearMarca()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearMarca(Marcas objetoMarca)
        {       

            bool bandera = GuardarMarca(objetoMarca);
            if(bandera == true)
            {
                TempData["MensajeIngreso"] = "Marca guardada";
                ModelState.Clear();
            }
            return View();
        }

        public MarcaController(DB_A49FE7_BaseDonacionesContext context)
        {
            _context = context;
        }

        public bool GuardarMarca(Marcas objetoMarca)
        {

            try
            {
                _context.Marcas.Add(objetoMarca);
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