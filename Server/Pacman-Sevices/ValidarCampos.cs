using System.Text.RegularExpressions;


namespace Pacman_Sevices
{
    /// <summary>Clase que tiene como funcionalidad validar la estructura de los datos introducidos por el usuario</summary>
    public class ValidarCampos
    {
        public enum ResultadosValidacion
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
        public ResultadosValidacion ValidarPassword(string contraseña)
        {
            string patrón = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,50}$";

            if (Regex.IsMatch(contraseña, patrón))
            {
                return ResultadosValidacion.ContraseñaVálida;
            }
            return ResultadosValidacion.ContraseñaInválida;

        }
        /// <summary>  Validar que el correo tenga una estructura válida</summary>
        /// <param name="correo">el correo.</param>
        /// <returns>Resultado de la validación</returns>
        public ResultadosValidacion ValidarCorreo(string correo)
        {
            string patrón = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            if (Regex.IsMatch(correo, patrón))
            {
                return ResultadosValidacion.CorreoVálido;
            }
            return ResultadosValidacion.Correoinválido;
        }

    }
}
