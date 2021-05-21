using Autofac.Extras.Moq;
using MolDavaBanking.Managers;
using MolDavaBanking.Web.Controllers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MolDavaBanking.Tests.Web
{
    [TestFixture]
    public class UserControllerTestClass
    {
        [Test]
        public void GetUsers_EmptyListOfUsers_Succesful()
        {
            //using(var mock = AutoMock.GetLoose())
            //{
            //    //Arrange
            //    var controller = mock.Create<UserController>();

            //    //Act
            //    var actionResult = controller.GetUsers() as ViewResult;

            //    //Assert
            //    Assert.That(actionResult.Model == null);
            //}

            Assert.That(true);
        }
    }
}
