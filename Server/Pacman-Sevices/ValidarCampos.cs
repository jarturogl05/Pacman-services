using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pacman_Sevices
{
    public class ValidarCampos
    {
        public enum ResultadosValidación
        {
            ContraseñaInválida,
            ContraseñaVálida,
            Correoinválido,
            CorreoVálido,
            UsernameInválido,
            UsernameVálido,
            NombreVálido,
            NombreInválido


        }


        /// <summary>  Valida la estructura correcta de una contraseña. Debe contener por lo menos 8 caracteres, una mayúscula y un número</summary>
        /// <param name="contraseña">  contraseña.</param>
        /// <returns>Resultado de la validación</returns>
        public ResultadosValidación ValidarPassword(string contraseña)
        {
            string patrón = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$";

            if (Regex.IsMatch(contraseña, patrón))
            {
                return ResultadosValidación.ContraseñaVálida;
            }
            return ResultadosValidación.ContraseñaInválida;

        }

        public ResultadosValidación ValidarCorreo(string correo)
        {
            string patrón = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (Regex.IsMatch(correo, patrón))
            {
                return ResultadosValidación.CorreoVálido;
            }
            return ResultadosValidación.Correoinválido;
        }

    }
}
