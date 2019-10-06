using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    Usuario = new Usuario{
                         Username = jugador.Username,
                         Password = jugador.Password
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
    }
}
