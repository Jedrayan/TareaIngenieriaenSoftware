using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FurmularioDonaciones.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurmularioDonaciones.Controllers
{
    public class LoginController : Controller
    {
        private DB_A49FE7_BaseDonacionesContext _context;
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string nombreUsuario, string usuarioContrasena)

        {
            var contrasenaEncrip = encriptarContrasena(usuarioContrasena);
            var objetoUsuario = _context.Usuario.Where(x => x.NombreUsuario.Equals(nombreUsuario) & x.PasswordUsuario.Equals(contrasenaEncrip)).FirstOrDefault();
            

            if (objetoUsuario != null)
            {
                HttpContext.Session.SetString("UsuarioSesion", System.Text.Json.JsonSerializer.Serialize(objetoUsuario));

                return RedirectToAction("FormularioDonacionCreate", "FormularioDonacion");
                
            }
            else
            {
                TempData["MensajeErrorIngreso"] = "Usuario o Contraseña incorrecto";
            }

            
            return View();
        }

        public LoginController(DB_A49FE7_BaseDonacionesContext context)
        {
            _context = context;
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
    }
}