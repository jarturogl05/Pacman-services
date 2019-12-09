using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pacman_Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman_Sevices.Tests
{
    [TestClass()]
    public class ScoreTests
    {
        [TestMethod()]
        public void GetScoreTest()
        {
            Services services = new Services();
            IScoreService.User user = new IScoreService.User();
            user.Nombre = "bestricks";
            user.Puntuación = 2;
            Assert.AreEqual(2, services.GetScore(user));
        }
        [TestMethod()]
        public void SetScoreTestLess()
        {
            Services services = new Services();
            IScoreService.User user = new IScoreService.User();
            user.Nombre = "bestricks";
            user.Puntuación = 2;
            Assert.AreEqual(false, services.SetScore(user, 2));
        }
        [TestMethod()]
        public void SetScoreTestGreater()
        {
            Services services = new Services();
            IScoreService.User user = new IScoreService.User();
            user.Nombre = "bestricks";
            user.Puntuación = 4;
            Assert.AreEqual(true, services.SetScore(user, 2));
        }
    }
}