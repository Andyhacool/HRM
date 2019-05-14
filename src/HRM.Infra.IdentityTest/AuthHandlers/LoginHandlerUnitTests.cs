using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Interfaces;
using HRM.Domain.Models.Identity;
using HRM.Infra.CrossCutting.Identity.AuthHandlers;
using HRM.Infra.CrossCutting.Identity.Dto;
using HRM.Infra.CrossCutting.Identity.Interfaces.Services;
using HRM.Infra.CrossCutting.Identity.Presenters;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace HRM.Infra.IdentityUnitTests.AuthHandlers
{
    public class LoginHandlerUnitTests
    {
        [Fact]
        public async void Handle_GivenValidCredentials_ShouldReturnObject()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).ReturnsAsync(new User(Guid.NewGuid(), "", "", "", "", ""));

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AccessToken("1234", 0));

            var mockTokenFactory = new Mock<ITokenFactory>();
            var presenter = new LoginPresenter();

            var mockMediatorHandler = new Mock<IMediatorHandler>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Commit()).Returns(true);

            var notificationHandler = new DomainNotificationHandler();
           
            var handler = new LoginHandler(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object, mockUnitOfWork.Object, mockMediatorHandler.Object, notificationHandler);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            await handler.Handle(new LoginRequest("userName", "password", "127.0.0.1"), presenter);

            // assert 
            Assert.NotNull(presenter.data);
        }

        [Fact]
        public async void Handle_GivenIncompleteCredentials_ShouldReturnNull()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).ReturnsAsync(new User(Guid.NewGuid(), "", "", "", "", ""));

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AccessToken("", 0));

            var mockTokenFactory = new Mock<ITokenFactory>();

            var presenter = new LoginPresenter();

            var mockMediatorHandler = new Mock<IMediatorHandler>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Commit()).Returns(true);

            var notificationHandler = new DomainNotificationHandler();

            var handler = new LoginHandler(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object, mockUnitOfWork.Object, mockMediatorHandler.Object, notificationHandler);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            await handler.Handle(new LoginRequest("", "password", "127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.Null(presenter.data);
            mockTokenFactory.Verify(factory => factory.GenerateToken(32), Times.Never);
        }


        [Fact]
        public async void Handle_GivenUnknownCredentials_ShouldFail()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).ReturnsAsync((User)null);

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AccessToken("", 0));

            var mockTokenFactory = new Mock<ITokenFactory>();

            var presenter = new LoginPresenter();

            var mockMediatorHandler = new Mock<IMediatorHandler>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Commit()).Returns(true);

            var notificationHandler = new DomainNotificationHandler();

            var handler = new LoginHandler(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object, mockUnitOfWork.Object, mockMediatorHandler.Object, notificationHandler);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            await handler.Handle(new LoginRequest("", "password", "127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.Null(presenter.data);
            mockTokenFactory.Verify(factory => factory.GenerateToken(32), Times.Never);
        }

        [Fact]
        public async void Handle_GivenInvalidPassword_ShouldFail()
        {
            // arrange
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.FindByName(It.IsAny<string>())).ReturnsAsync((User)null);

            mockUserRepository.Setup(repo => repo.CheckPassword(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AccessToken("", 0));

            var mockTokenFactory = new Mock<ITokenFactory>();

            var presenter = new LoginPresenter();

            var mockMediatorHandler = new Mock<IMediatorHandler>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Commit()).Returns(true);

            var notificationHandler = new DomainNotificationHandler();

            var handler = new LoginHandler(mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object, mockUnitOfWork.Object, mockMediatorHandler.Object, notificationHandler);

            var mockOutputPort = new Mock<IOutputPort<LoginResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<LoginResponse>()));

            // act
            await handler.Handle(new LoginRequest("", "password", "127.0.0.1"), mockOutputPort.Object);

            // assert
            Assert.Null(presenter.data);
            mockTokenFactory.Verify(factory => factory.GenerateToken(32), Times.Never);
        }
    }
}
