using HRM.Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HRM.Domain.UnitTests.Entities
{
   public class UserUnitTests
    {
        [Fact]
        public void HasValidRefreshToken_GivenValidToken_ShouldReturnTrue()
        {
            // arrange
            const string refreshToken = "1234";
            var userId = Guid.NewGuid();
            var user = new User(userId, "", "", "", "", "");
            user.AddRefreshToken(refreshToken, userId, "127.0.0.1");

            // act
            var result = user.HasValidRefreshToken(refreshToken);

            Assert.True(result);
        }

        [Fact]
        public void HasValidRefreshToken_GivenExpiredToken_ShouldReturnFalse()
        {
            // arrange
            const string refreshToken = "1234";
            var userId = Guid.NewGuid();
            var user = new User(userId, "", "", "", "", "");
            user.AddRefreshToken(refreshToken, userId, "127.0.0.1", -6); // Provision with token 6 days old

            // act
            var result = user.HasValidRefreshToken(refreshToken);

            Assert.False(result);
        }

        [Fact]
        public void HasValidRefreshToken_AfterRemoveRefreshToken_ShouldReturnFalse()
        {
            // arrange
            const string refreshToken = "1234";
            var userId = Guid.NewGuid();
            var user = new User(userId, "", "", "", "", "");
            user.AddRefreshToken(refreshToken, userId, "127.0.0.1", -6); // Provision with token 6 days old
            user.RemoveRefreshToken(refreshToken);

            // act
            var result = user.HasValidRefreshToken(refreshToken);

            Assert.False(result);
        }
    }
}
