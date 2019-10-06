using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pac_man_host
{
    [ServiceContract]
    public interface IScoreService
    {

        [OperationContract]
        bool SetScore(User user, double score);

        [OperationContract]
        Double GetScore(User user);


        [DataContract]
        public class User
        {
            private String username;
            private Double puntuación;



            [DataMember]
            public String Nombre { get { return username; } set { username = value; } }


            [DataMember]
            public Double Puntuación { get { return puntuación; } set { puntuación = value; } }


        }


    }
}
