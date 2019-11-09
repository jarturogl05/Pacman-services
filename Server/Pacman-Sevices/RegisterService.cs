using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices

{
    public class RegisterService : IRegisterService
    {
        public int AddUser(IRegisterService.Jugador jugador)
        {
            DataAccess.ModelContainer container = new ModelContainer();
            ICollection<Jugador> usuarios = new List<Jugador>
            {
                new Jugador{
                    Nombre = jugador.Nombre,
                    Correo = jugador.Correo,
                    Elo = "",
                    PuntuaciónAlta = "",
                    Puntuación = "",
                    PantallasGanadas = "",

                    Ranking = new Ranking
                    {
                        Posicion = "0"
                    },
                    Usuario = new Usuario{
                         Username = jugador.Username,
                         Password = PassHash(jugador.Password)
                 }
                },

            };

            foreach (var usuario in usuarios)  
            {
                container.JugadorSet.Add(usuario);
            }
            container.SaveChanges();
            
            return 1;
        }

        DBOperationResult.AddResult IRegisterService.AddUser(IRegisterService.Jugador jugador)
        {
            throw new NotImplementedException();
        }

        /// <summary>Hashea un parametro ingresado.</summary>
        /// <param name="data">El parametro.</param>
        /// <returns>El parametro en SHA1</returns>
        private String PassHash(String data)
        {
            SHA1 sha = SHA1.Create();
            byte[] hashData = sha.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder stringBuilderValue = new StringBuilder();

            for (int i = 0; i < hashData.Length; i++)
            {
                stringBuilderValue.Append(hashData[i].ToString());
            }
            return stringBuilderValue.ToString();
        }
    }
}
