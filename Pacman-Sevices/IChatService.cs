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
    public interface IChatService
    {

        [OperationContract]
        void Say(Message msg);

        [OperationContract]
        void Receive(Message msg);

        [DataContract]
        public class Message
        {
            private string content;
            private string sender;

            [DataMember]
            public string Sender { get { return sender; } set { sender = value; } }

            [DataMember]
            public string Content { get { return content; } set { content = value; } }

        }
    }
}
