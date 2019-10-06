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
    public interface IRegisterService
    {
        [OperationContract]
        int AddUser(User user);

        [DataContract]
        public class User
        {
            private String username;
            private String email;
            private String password;

            [DataMember]
            public String Nombre { get { return username; } set { username = value; } }

            [DataMember]
            public String Correo { get { return email; } set { email = value; } }

            [DataMember]
            public String Password { get { return password; } set { password = value; } }

        }
    }
}
