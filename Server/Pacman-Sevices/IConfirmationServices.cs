﻿using DataAccess;
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
    public interface IConfirmationServices
    {
        [OperationContract]
        int SendEmail(Jugador jugador);

        [OperationContract]
        int GenerateNewCode(Jugador jugador);

        [DataContract]
        public class Jugador
        {
            private String email;
            private Usuario usuario;
            private String código;

            [DataMember]
            public String Correo { get { return email; } set { email = value; } } 

            [DataMember]
            public Usuario Usuario { get { return usuario; } set { usuario = value; } }

            [DataMember]
            public String Código { get { return código; } set { código = value; } }

        }


        }

    }

