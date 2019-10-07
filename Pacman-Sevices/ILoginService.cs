using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices
{
    [ServiceContract]
    public interface ILoginService
    {
        [OperationContract]
        int ValidateUser(Usuario usuario);

        [DataContract]
        public class Usuario
        {
            private String username;
            private String password;

            [DataMember]
            public String Username { get { return username; } set { username = value; } }

            [DataMember]
            public String Password { get { return password; } set { password = value; } }

        }
    }
}
