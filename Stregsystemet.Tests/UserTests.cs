using System.Collections.Generic;
using Xunit;
using System;
using System.Reflection;

namespace Stregsystemet.Tests
{
    public class UserTests : IDisposable
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(10, 10)]
        public void UserIDIncrementsFromStaticValue(int numberOfUsers, int expectedUserId)
        {

            List<User> users = new List<User>();
            for (var i = 0; i < numberOfUsers; i++)
            {
                users.Add(new User("", "", "", "", 0));
            }
            Assert.Equal(expectedUserId, users[users.Count - 1].ID);
        }
        [Fact]
        public void Firstname_IsNotNull()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            Assert.NotNull(user.FirstName);
        }
        [Fact]
        public void Lastname_IsNotNull()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            Assert.NotNull(user.FirstName);
        }

        [Theory]
        [InlineData("sigurd")]
        [InlineData("s1gurd")]
        [InlineData("1337")]
        [InlineData("4206969gotem")]
        [InlineData("sigurd_")]
        public void ValidDomain_validateUsername_Accepts_Username(string username)
        {
            User user = new User("Test", "Name", username, "test.name@gmail.com", 0);
            Assert.Equal(username, user.Username);
        }

        [Theory]
        [InlineData("Sigurd")]
        [InlineData("s!gurd")]
        [InlineData("si@urd")]
        [InlineData("johannes-sigurd")]
        [InlineData("\n")]
        [InlineData("FunnyName:)")]
        [InlineData("\\n")]
        [InlineData("\0")]
        public void InvalidDomain_validateUsername_ThrowsException(string username)
        {
            Assert.Throws<InvalidCharacterException>(() => new User("Test", "Name", username, "test.name@gmail.com", 0));
        }

        //Reset static ID value after every test run
        public void Dispose()
        {
            FieldInfo? field = typeof(User).GetField("s_ID", BindingFlags.Static | BindingFlags.NonPublic);
            if (field != null)
            {
                field.SetValue(null, 0);
            }
        }
    }

}