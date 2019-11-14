﻿using System;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Pacman_Sevices
{
    [ServiceContract]
    public interface IRegisterService
    {
        [OperationContract]
        DBOperationResult.AddResult AddUser(Jugador jugador);

        [OperationContract]
        DBOperationResult.AddResult SerachUserInDB(Jugador jugador);

        [DataContract]
        public class Jugador
        {
            private String nombre;
            private String username;
            private String email;
            private String password;
            private string código;

            [DataMember]
            public String Nombre { get { return nombre; } set { nombre = value; } }

            [DataMember]
            public String Username { get { return username; } set { username = value; } }

            [DataMember]
            public String Correo { get { return email; } set { email = value; } }

            [DataMember]
            public String Password { get { return password; } set { password = value; } }

            [DataMember]
            public String Código { get { return código; } set { código = value; } }

        }
    }
}
