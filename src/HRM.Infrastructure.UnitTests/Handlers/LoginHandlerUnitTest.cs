using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Interfaces;
using HRM.Domain.Models.Identity;
using HRM.Infra.CrossCutting.Identity.AuthHandlers;
using HRM.Infra.CrossCutting.Identity.Dto;
using HRM.Infra.CrossCutting.Identity.Interfaces.Services;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HRM.Infrastructure.UnitTests.Handlers
{
    [TestClass]
    public class LoginHandlerUnitTest
    {
        [Ignore]
        [TestMethod]
        public async Task Handle_GivenValidCredentials_ShouldSucceed()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).ReturnsAsync(new User(Guid.NewGuid(), "", "", "", "", ""));

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AccessToken("", 0));

            var mockTokenFactory = new Mock<ITokenFactory>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMediatorHandler = new Mock<IMediatorHandler>();
            var mockNotificationHandler = new Mock<DomainNotificationHandler>();

            var useCase = new LoginHandler(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object, mockUnitOfWork.Object, mockMediatorHandler.Object, mockNotificationHandler.Object);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            await useCase.Handle(new LoginRequest("userName", "password", "127.0.0.1"), mockOutputPort.Object);

            // assert- no error
           
        }
    }
}
