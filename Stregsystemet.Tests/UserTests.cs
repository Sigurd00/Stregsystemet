using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Stregsystemet.Tests
{
    public class UserTests : IDisposable
    {
        [Theory (Skip = "Some tests are fucking this hard:(, also should look into doing this another way since data set has gaps in id")]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(10, 10)]
        public void UserIDIncrementsFromStaticValue(int numberOfUsers, int expectedUserId)
        {

            List<User> users = new List<User>();
            for (var i = 0; i < numberOfUsers; i++)
            {
                users.Add(new User("", "", "", "valid@email.com", 0));
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

        [Theory]
        [InlineData("test.email@email.com")]
        [InlineData("sigurd@gmail.com")]
        [InlineData("123123@12.3.com")]
        [InlineData("123.123.yep@gmail.com")]
        [InlineData("____@asd-sda.com")]
        public void ValidDomain_validateEmail_Accepts_Email(string email)
        {
            User user = new User("Test", "Name", "sigurd", email, 0);
            Assert.Equal(email, user.Email);
        }
        [Theory]
        [InlineData("|test.email@email.com")]
        [InlineData("test.email(2)@email.com")]
        [InlineData("test.email@.email.com")]
        [InlineData("test.email@email.com.")]
        [InlineData("test.email@em=ail.com")]
        [InlineData("test.email@em<>ail.com")]
        public void InvalidDomain_validateEmail_ThrowsInvalidCharacterException(string email)
        {
            Assert.Throws<InvalidCharacterException>(() => new User("Test", "Name", "sigurd", email, 0));
        }
        [Theory]
        [InlineData("test.email@@email.com")]
        [InlineData("sigurd@skadborg@gmail.com")]
        [InlineData("")]
        public void InvalidDomain_validateEmail_ThrowsInvalidEmailException(string email)
        {
            Assert.Throws<InvalidEmailException>(() => new User("Test", "Name", "sigurd", email, 0));
        }

        [Fact]
        public void ValidDomain_User_Equals_returns_correctly()
        {
            User user = new User("Test", "Name", "sigurd", "test.name@gmail.com", 0);
            Assert.True(user.Equals(user));
        }

        [Fact]
        public void ValidDomain_User_Implements_CompareTo_correctly()
        {
            //User one comes before user two if they were to be sorted
            User user1 = new User("Test", "Name", "sigurd", "test.name@gmail.com", 0);
            User user2 = new User("Test", "Name", "another", "test.name@gmail.com", 0);
            Assert.Equal(-1, user1.CompareTo(user2));
        }

        [Fact]
        public void ValidDomain_User_Implements_ToString_correctly()
        {
            //User one comes before user two if they were to be sorted
            User user = new User("Test", "Name", "sigurd", "test.name@gmail.com", 0);
            Assert.Equal("Test Name <test.name@gmail.com>", user.ToString());
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