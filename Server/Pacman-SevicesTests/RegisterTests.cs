using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pacman_SevicesTests;

namespace Pacman_Sevices.Tests
{
    [TestClass()]
    public class RegisterServicesTests
    {
        [TestMethod()]
        public void AddUserTest()
        {

            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jarturogl05@gmail.com";
            jugador.Nombre = "Arturo";
            jugador.Username = "pep777e";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");           
            Assert.AreEqual(DBOperationResult.AddResult.Success, services.AddUser(jugador));
        }

    }
}