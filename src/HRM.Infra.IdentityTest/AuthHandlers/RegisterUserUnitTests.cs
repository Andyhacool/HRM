using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Interfaces;
using HRM.Infra.CrossCutting.Identity.Models;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.UseCaseHandlers;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HRM.Infra.IdentityUnitTests.AuthHandlers
{
    public class RegisterUserUnitTests
    {
        [Fact]
        public async void Handle_GivenValidRegistrationDetails_ShouldSucceed()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            List<AppUser> users = new List<AppUser>
             {
                  new AppUser() { Email = "Test@gmail.com" }
             };

            var mockUserManager = MockUserManager<AppUser>(users);

            var mockMediatorHandler = new Mock<IMediatorHandler>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Commit()).Returns(true);

            var notificationHandler = new DomainNotificationHandler();

            var useHandler = new RegisterUserHandler(mockUserRepository.Object, mockUserManager.Object, mockUnitOfWork.Object, mockMediatorHandler.Object, notificationHandler);

            // act 
            try
            {
                await useHandler.Handle(new RegisterUserRequest("firstName", "lastName", "me@domain.com", "userName", "password"));
            }
            catch (Exception ex)
            {
                // assert
                Assert.True(false, "Expected no exception, but got: " + ex.Message);
            }                      
        }

        public static Mock<UserManager<TUser>> MockUserManager<TUser>(List<TUser> ls) where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            var mgr = new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
            mgr.Object.UserValidators.Add(new UserValidator<TUser>());
            mgr.Object.PasswordValidators.Add(new PasswordValidator<TUser>());

            mgr.Setup(x => x.DeleteAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);
            mgr.Setup(x => x.CreateAsync(It.IsAny<TUser>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success).Callback<TUser, string>((x, y) => ls.Add(x));
            mgr.Setup(x => x.UpdateAsync(It.IsAny<TUser>())).ReturnsAsync(IdentityResult.Success);

            return mgr;
        }
    }
}
