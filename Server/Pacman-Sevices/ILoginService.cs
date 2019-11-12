using System;
using System.Runtime.Serialization;
using System.ServiceModel;


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
