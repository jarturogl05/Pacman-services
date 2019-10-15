using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices
{
    public class ServerUser
    {
        public int ID { get; set; }
        public String Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
