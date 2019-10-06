using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Pacman_Sevices;


namespace Host
{
    class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Pacman_Sevices.RegisterService)))
            {

                host.Open();
                Console.WriteLine("Server is running");
                Console.ReadLine();
            }

        }
    }
}
