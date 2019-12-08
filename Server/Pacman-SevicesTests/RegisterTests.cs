using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pacman_SevicesTests;

namespace Pacman_Sevices.Tests
{
    [TestClass()]
    public class RegisterServicesTests
    {

        [TestMethod()]
        public void AddUserTestNoConection()
        {

            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Nombre = "Arturo";
            jugador.Username = "pessp7e";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");           
            Assert.AreEqual(DBOperationResult.AddResult.SQLError, services.AddUser(jugador));
        }

        [TestMethod()]
        public void AddUserTest()
        {

            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Nombre = "Arturo";
            jugador.Username = "pessp7e";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");
            Assert.AreEqual(DBOperationResult.AddResult.Success, services.AddUser(jugador));
        }

        [TestMethod()]
        public void AddWrongInvalidUserTest()
        {

            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Nombre = "";
            jugador.Username = "";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");
            Assert.AreEqual(DBOperationResult.AddResult.NullObject, services.AddUser(jugador));
        }

        [TestMethod()]
        public void SerarchExistingUserTestNoConection()
        {

            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Nombre = "Arturo";
            jugador.Username = "pessp7e";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");
            Assert.AreEqual(DBOperationResult.AddResult.SQLError, services.SerachUserInDB(jugador));
        }


        [TestMethod()]
        public void SerarchExistingUserTest()
        {
            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jarturzgl5@gmail.com";
            jugador.Nombre = "Arturo";
            jugador.Username = "pessp7e";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");
            Assert.AreEqual(DBOperationResult.AddResult.ExistingRecord, services.SerachUserInDB(jugador));
        }



        [TestMethod()]
        public void SerarchNewUserTest()
        {
            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jargl5@gmail.com";
            jugador.Nombre = "Arturo";
            jugador.Username = "p7e";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");
            Assert.AreEqual(DBOperationResult.AddResult.Success, services.SerachUserInDB(jugador));
        }

        [TestMethod()]
        public void SerarchInvalidUserTest()
        {
            Pacman_Sevices.Services services = new Services();
            Pacman_Sevices.IRegisterService.Jugador jugador = new IRegisterService.Jugador();
            System.Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            jugador.Correo = "jargl5@gmail.com";
            jugador.Nombre = "Arturo";
            jugador.Username = "";
            jugador.Password = hashPass.HashPassword("ElreyOtak02");
            jugador.Código = generator.Next(0, 999999).ToString("D6");
            Assert.AreEqual(DBOperationResult.AddResult.NullObject, services.SerachUserInDB(jugador));
        }


    }
}