using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices
{
    public class LoginService : ILoginService
    {
        public int ValidateUser(ILoginService.Usuario usuario)
        {
            ModelContainer container = new ModelContainer();
            foreach (var Usuario in container.UsuarioSet)
            {
                if (Usuario.Username == usuario.Username && Usuario.Password == usuario.Password)
                {
                    return 1;
                }
            }
            return 0;
        }
    }
}
