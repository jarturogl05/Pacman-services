using System;
using System.ServiceModel;
using Pacman_Sevices;


namespace Host
{
   public static class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost host = new ServiceHost(typeof(Services)))
            {
                host.Open();
                Console.WriteLine("Server is running");
                Console.ReadLine();
            }

        }
    }
}
