using System;
using System.ServiceModel;


namespace Pacman_Sevices
{
    public class ServerUser
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
