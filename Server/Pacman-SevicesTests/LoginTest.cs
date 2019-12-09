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
    public class LoginServicesTests
    {

        [TestMethod()]
        public void GetEmailTestNoConection()
        {
            Services services = new Services();
            ILoginService.Usuario usuario = new ILoginService.Usuario();
            Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            usuario.Username = "pessp7e";
            usuario.Password = hashPass.HashPassword("ElreyOtak02");            
            Assert.AreEqual(null, services.GetEmail(usuario));
        }

        [TestMethod()]
        public void GetEmailTest()
        {
            Services services = new Services();
            ILoginService.Usuario usuario = new ILoginService.Usuario();
            Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            usuario.Username = "pessp7e";
            usuario.Password = hashPass.HashPassword("ElreyOtak02");
            Assert.AreEqual("jarturzgl5@gmail.com", services.GetEmail(usuario));
        }

        [TestMethod()]
        public void LoginTestNoConection()
        {
            Services services = new Services();
            ILoginService.Usuario usuario = new ILoginService.Usuario();
            Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            usuario.Username = "pessp7e";
            usuario.Password = hashPass.HashPassword("ElreyOtak02");
            Assert.AreEqual(DBOperationResult.AddResult.SQLError, services.ValidateUser(usuario));
        }

        [TestMethod()]
        public void LoginTestNotConfirmed()
        {
            Services services = new Services();
            ILoginService.Usuario usuario = new ILoginService.Usuario();
            Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            usuario.Username = "pessp7e";
            usuario.Password = hashPass.HashPassword("ElreyOtak02");
            Assert.AreEqual(DBOperationResult.AddResult.ConfirmationIsFalse, services.ValidateUser(usuario));
        }

        [TestMethod()]
        public void LoginTestNotRegistered()
        {
            Services services = new Services();
            ILoginService.Usuario usuario = new ILoginService.Usuario();
            Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            usuario.Username = "pxxxxxxxxxxe";
            usuario.Password = hashPass.HashPassword("ElreyOtak02");
            Assert.AreEqual(DBOperationResult.AddResult.WrongCredentials, services.ValidateUser(usuario));
        }


        [TestMethod()]
        public void LoginTest()
        {
            Services services = new Services();
            ILoginService.Usuario usuario = new ILoginService.Usuario();
            Random generator = new System.Random();
            HashPass hashPass = new HashPass();
            usuario.Username = "pessp7e";
            usuario.Password = hashPass.HashPassword("ElreyOtak02");
            Assert.AreEqual(DBOperationResult.AddResult.Success, services.ValidateUser(usuario));
        }



    }
}
