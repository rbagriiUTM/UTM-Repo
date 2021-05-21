using Autofac.Extras.Moq;
using MolDavaBanking.Domain.UserViewModel;
using MolDavaBanking.Managers;
using MolDavaBanking.Persistance;
using MolDavaBanking.Repositories.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MolDavaBanking.Tests.Managers
{
    [TestFixture]
    public class UserManagerTests
    {
        [Test]
        public void GetUsers_ListOfUsersIsNotNull_Succesful()
        {
            AutoMapper.InitializeMapper();

            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IUserRepository>()
                    .Setup(x => x.GetUsers())
                    .Returns(GetListOfUsers);

                var cls = mock.Create<UserManager>();

                //Act
                var expected = GetListOfUsers();

                var actual = cls.GetUsers();

                //Assert
                Assert.True(actual != null);

                Assert.AreEqual(expected.Count, actual.Count);
            }
        }

        [Test]
        public void GetUsers_ListOfUsersIsNull_Succesful()
        {
            using (var mock = AutoMock.GetLoose())
            {
                //Arrange
                mock.Mock<IUserRepository>()
                    .Setup(x => x.GetUsers())
                    .Returns(GetEmptyListOfUsers);

                var cls = mock.Create<UserManager>();

                //Act
                var expected = GetEmptyListOfUsers();

                var actual = cls.GetUsers();

                //Assert
                Assert.True(actual.Count == 0);
            }
        }

        private List<User> GetListOfUsers()
        {
            List<User> users = new List<User>
            {
                new User
                {
                    Adress = "str. Teilor",
                    BirthDate = DateTime.Now,
                    FirstName = "Test",
                    LastName = "Test",
                    Email = "test.test@mail.com",
                    IDNP = "7897897897897",
                    IsLocked = 0,
                    Password = "1qA!1qA!"
                },

                new User
                {
                    Adress = "str. Teilor",
                    BirthDate = DateTime.Now,
                    FirstName = "Testt",
                    LastName = "Testt",
                    Email = "testt.testt@mail.com",
                    IDNP = "1231231231231",
                    IsLocked = 0,
                    Password = "1qA!1qA!"
                },

                new User
                {
                    Adress = "str. Teilor",
                    BirthDate = DateTime.Now,
                    FirstName = "Testtt",
                    LastName = "Testtt",
                    Email = "testtt.testtt@mail.com",
                    IDNP = "4564564564564",
                    IsLocked = 0,
                    Password = "1qA!1qA!"
                }
            };

            return users;
        }

        private List<User> GetEmptyListOfUsers()
        {
            List<User> users = new List<User>();

            return users;
        }
    }
}
