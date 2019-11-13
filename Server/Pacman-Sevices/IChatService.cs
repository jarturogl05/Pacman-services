using System;
using System.ServiceModel;


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
        void SendMsg(String message, int id);
    }
    [ServiceContract]
    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(String message);
    }
}
