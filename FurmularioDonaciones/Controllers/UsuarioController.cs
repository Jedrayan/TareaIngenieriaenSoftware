using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FurmularioDonaciones.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using FurmularioDonaciones.ViewModel;
using System.Security.Cryptography;
using System.Text;

namespace FurmularioDonaciones.Controllers
{
    public class UsuarioController : Controller
    {
        private DB_A49FE7_BaseDonacionesContext _context;

        //GET: Crear Usuario
        public IActionResult CrearUsuario()

        {
            cargarRolesSelect();
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuario(Usuario objetoUsuario)

        {
            

            objetoUsuario.PasswordUsuario =  encriptarContrasena(objetoUsuario.PasswordUsuario); 




            bool bandera = GuardarUsuario(objetoUsuario);
            if (bandera)
            {
                TempData["MensajeExitoso"]="Usuario Guardado Correctamente.";
                ModelState.Clear();
            }
            else
            {
                TempData["MensajeFallido"] = "Error, el usuario no fue guardado.";

            }
            cargarRolesSelect();
            return View();
        }




        
        // Contexto DB
        public UsuarioController(DB_A49FE7_BaseDonacionesContext context)
        {
            _context = context;
        }



        // Funcion Cargar Roles Select
        public void cargarRolesSelect()
        {

            IEnumerable<Rol> listaRoles = _context.Rol;
            List<ModelSelect> listaSelect = new List<ModelSelect>();

            var listaFinalSelect = listaRoles.Select(x => new ModelSelect
            {
                value = x.IdRol,
                text = x.NombreRol

            });
            ViewBag.ListaRolesVista = listaFinalSelect;
        }
        public static string encriptarContrasena(string contrasena)
        {
            SHA256Managed encritado = new SHA256Managed();
            string encriptado1 = String.Empty;
            //Se cencripta la primera parte del codigo 
            byte[] crypto = encritado.ComputeHash(Encoding.ASCII.GetBytes(contrasena), 0, Encoding.ASCII.GetByteCount(contrasena));
            foreach (byte theByte in crypto)
            {
                encriptado1 += theByte.ToString("x2");
            }

            //Se ve necesario encriptar dos veces con diferente multiplicador
            string encriptado2 = "";
            foreach (byte theByte in crypto)
            {
                encriptado2 += theByte.ToString("x4");
            }

            string contrasenaEncriptada = encriptado1.ToString() + encriptado2.ToString();

            //Se regresa las dos cadenas y se corda 10 caracteres para aumentar la complejidad.
            return contrasenaEncriptada.Substring(0, contrasenaEncriptada.Length - 10);
        }//encriptarContrasena


        // Funcion Guardar Nuevo Usuario

        public bool GuardarUsuario(Usuario objetoUsuario)
        {

            try
            {
                _context.Usuario.Add(objetoUsuario);
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