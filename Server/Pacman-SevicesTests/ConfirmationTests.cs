using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pacman_Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_SevicesTests
{
    [TestClass()]
    public class ConfirmationServicesTests
    {

        [TestMethod()]
        public void ChangeConfirmationStatusTestNoConection()
        {
            Services services = new Services();
            IConfirmationServices.Jugador jugador = new IConfirmationServices.Jugador();
            
            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Código = "244414";
            Assert.AreEqual(DBOperationResult.AddResult.SQLError, services.ChangeConfirmationStatus(jugador));
        }

        [TestMethod()]
        public void ChangeConfirmationStatusTest()
        {
            Services services = new Services();
            IConfirmationServices.Jugador jugador = new IConfirmationServices.Jugador();

            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Código = "244414";
            Assert.AreEqual(DBOperationResult.AddResult.Success, services.ChangeConfirmationStatus(jugador));
        }

        [TestMethod()]
        public void NewCodeTestNoConection()
        {
            Services services = new Services();
            IConfirmationServices.Jugador jugador = new IConfirmationServices.Jugador();

            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Código = "244414";
            Assert.AreEqual(DBOperationResult.AddResult.SQLError, services.GenerateNewCode(jugador));
        }


        [TestMethod()]
        public void NewCodeTest()
        {
            Services services = new Services();
            IConfirmationServices.Jugador jugador = new IConfirmationServices.Jugador();

            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Código = "663666";
            Assert.AreEqual(DBOperationResult.AddResult.Success, services.GenerateNewCode(jugador));
        }

        [TestMethod()]
        public void SendEmailTestNoConection()
        {
            Services services = new Services();
            IConfirmationServices.Jugador jugador = new IConfirmationServices.Jugador();

            jugador.Correo = "muterk05@gmail.com";
            jugador.Código = "663666";
            Assert.AreEqual(0, services.SendEmail(jugador));
        }

        [TestMethod()]
        public void SendEmailTest()
        {
            Services services = new Services();
            IConfirmationServices.Jugador jugador = new IConfirmationServices.Jugador();

            jugador.Correo = "muterk05@gmail.com";
            jugador.Código = "663666";
            Assert.AreEqual(1, services.SendEmail(jugador));
        }



    }
}
