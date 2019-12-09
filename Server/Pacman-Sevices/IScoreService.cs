using System;
using System.Runtime.Serialization;
using System.ServiceModel;


namespace Pacman_Sevices

{
    [ServiceContract]
    public interface IScoreService
    {

        [OperationContract]
        bool SetScore(User user, double score);

        [OperationContract]
        int GetScore(User user);

        [DataContract]
        public class User
        {
            private String username;
            private int puntuación;



            [DataMember]
            public String Nombre { get { return username; } set { username = value; } }


            [DataMember]
            public int Puntuación { get { return puntuación; } set { puntuación = value; } }


        }


    }
}
