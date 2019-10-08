using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Pacman_Sevices;


namespace Host
{
   public class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Pacman_Sevices.ChatService)))
            {
                host.Open();
                Console.WriteLine("Server is running");
                Console.ReadLine();
            }

        }
    }
}
