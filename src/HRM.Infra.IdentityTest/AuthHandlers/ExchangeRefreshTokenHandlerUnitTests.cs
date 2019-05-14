using HRM.Domain.Core.Bus;
using HRM.Domain.Core.Notifications;
using HRM.Domain.Interfaces;
using HRM.Domain.Models.Identity;
using HRM.Infra.CrossCutting.Identity.Dto;
using HRM.Infra.CrossCutting.Identity.Interfaces.Services;
using HRM.Infra.CrossCutting.Identity.Presenters;
using HRM.Infra.CrossCutting.Identity.Requests;
using HRM.Infra.CrossCutting.Identity.Responses;
using HRM.Infra.CrossCutting.Identity.Specifications;
using HRM.Infra.CrossCutting.Identity.UseCaseHandlers;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Xunit;

namespace HRM.Infra.IdentityUnitTests.AuthHandlers
{
    public class ExchangeRefreshTokenHandlerUnitTests
    {
        [Fact]
        public async void Handle_GivenInvalidToken_ShouldReturnNull()
        {
            // arrange
            var mockJwtTokenValidator = new Mock<IJwtTokenValidator>();
            mockJwtTokenValidator.Setup(validator => validator.GetPrincipalFromToken(It.IsAny<string>(), It.IsAny<string>())).Returns((ClaimsPrincipal)null);

            var presenter = new ExchangeRefreshTokenPresenter();

            var mockUserRepository = new Mock<IUserRepository>();
            var mockMediatorHandler = new Mock<IMediatorHandler>();

            var handler = new ExchangeRefreshTokenHandler(mockJwtTokenValidator.Object, null, null, null, null, mockMediatorHandler.Object, null);

            // act
            await handler.Handle(new ExchangeRefreshTokenRequest("", ""), presenter);

            // assert
            Assert.Null(presenter.data);
        }

        [Fact]
        public async void Handle_GivenValidToken_ShouldReturnObject()
        {
            // arrange
            var mockJwtTokenValidator = new Mock<IJwtTokenValidator>();
            mockJwtTokenValidator.Setup(validator => validator.GetPrincipalFromToken(It.IsAny<string>(), It.IsAny<string>())).Returns(new ClaimsPrincipal(new[]
            {
                new ClaimsIdentity(new []{ new Claim("id","111-222-333")})
            }));

            const string refreshToken = "1234";
            var userId = Guid.NewGuid();
            var user = new User(userId, "", "", "", "", "");
            user.AddRefreshToken(refreshToken, userId, "");

            var presenter = new ExchangeRefreshTokenPresenter();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetSingleBySpec(It.IsAny<UserSpecification>())).ReturnsAsync(user);

            var mockJwtFactory = new Mock<IJwtFactory>();
            mockJwtFactory.Setup(factory => factory.GenerateEncodedToken(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new AccessToken("", 0));

            var mockTokenFactory = new Mock<ITokenFactory>();
            mockTokenFactory.Setup(factory => factory.GenerateToken(32)).Returns("");

            var mockOutputPort = new Mock<IOutputPort<ExchangeRefreshTokenResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<ExchangeRefreshTokenResponse>()));

            var mockMediatorHandler = new Mock<IMediatorHandler>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(uow => uow.Commit()).Returns(true);

            var notificationHandler = new DomainNotificationHandler();

            var handler = new ExchangeRefreshTokenHandler(mockJwtTokenValidator.Object, mockUserRepository.Object, mockJwtFactory.Object, mockTokenFactory.Object, mockUnitOfWork.Object, mockMediatorHandler.Object, notificationHandler);

            // act
            await handler.Handle(new ExchangeRefreshTokenRequest("4567", refreshToken), presenter);

            // assert
            Assert.NotNull(presenter.data);
        }
    }
}
