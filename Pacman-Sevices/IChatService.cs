using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices
{
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IChatService
    {
        [OperationContract]
        int Connect(String name);
        [OperationContract]
        void Disconnect(int id);
        [OperationContract(IsOneWay = true)]
        void SendMsg(String msg, int id);
    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(String msg);
    }
}
