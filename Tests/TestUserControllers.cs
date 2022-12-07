﻿using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations.Rules;

namespace Tests;

public class TestUserControllers
{
	[Fact]
	public void Test_01()
	{
		// 
		var options =
			new DbContextOptionsBuilder<Data.DatabaseContext>()
			.UseInMemoryDatabase(databaseName: "UsersControllerTest")
			.ConfigureWarnings(current => current.Ignore
			(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		using var databaseContext =
			new Data.DatabaseContext(options: options);

		databaseContext.Database.EnsureDeleted();
		databaseContext.Database.EnsureCreated();

		//databaseContext.AddRange(
		//	new Blog { Name = "Blog1", Url = "http://blog1.com" },
		//	new Blog { Name = "Blog2", Url = "http://blog2.com" });

		databaseContext.SaveChanges();

		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();

		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns("192.168.1.110");

		var usersController = new Server.Controllers.UsersController
			(logger: mockLogger.Object, databaseContext: databaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.DepositeRequestDto();

		request.User.IP = "1.1.1.1";
		request.User.AdditionalData = null;
		request.User.NationalCode = "1234512345";
		request.User.DisplayName = "داریوش تصدیقی";
		request.User.CellPhoneNumber = "09121087461";
		request.User.EmailAddress = "DariushT@GMail.com";

		request.Amount = 10;
		request.WithdrawDurationInDays = 0;

		request.AdditionalData = null;
		request.ProviderName = "ایران کیش";
		request.ReferenceCode = "1234567890";
		request.SystemicDescription = "شارژ کیف پول";
		request.UserDescription = "دوست دارم کیف پولم رو شارژ کنم";

		request.WalletToken = System.Guid.NewGuid();
		request.CompanyToken = System.Guid.NewGuid();

		var result =
			usersController.Deposite(request: request) as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as Dtat.Result;

		Assert.NotNull(@object: result);

		Assert.False(condition: value!.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.ThereIsNotAnyItemWithThisToken,
			arg0: "company");

		Assert.Equal(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void Test_02()
	{
		// 
		var options =
			new DbContextOptionsBuilder<Data.DatabaseContext>()
			.UseInMemoryDatabase(databaseName: "UsersControllerTest")
			.ConfigureWarnings(current => current.Ignore
			(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		using var databaseContext =
			new Data.DatabaseContext(options: options);

		databaseContext.Database.EnsureDeleted();
		databaseContext.Database.EnsureCreated();

		//databaseContext.AddRange(
		//	new Blog { Name = "Blog1", Url = "http://blog1.com" },
		//	new Blog { Name = "Blog2", Url = "http://blog2.com" });

		databaseContext.SaveChanges();

		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();

		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns("192.168.1.110");

		var usersController = new Server.Controllers.UsersController
			(logger: mockLogger.Object, databaseContext: databaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.DepositeRequestDto();

		request.User.IP = "1.1.1.1";
		request.User.AdditionalData = null;
		request.User.NationalCode = "1234512345";
		request.User.DisplayName = "داریوش تصدیقی";
		request.User.CellPhoneNumber = "09121087461";
		request.User.EmailAddress = "DariushT@GMail.com";

		request.Amount = 10;
		request.WithdrawDurationInDays = 0;

		request.AdditionalData = null;
		request.ProviderName = "ایران کیش";
		request.ReferenceCode = "1234567890";
		request.SystemicDescription = "شارژ کیف پول";
		request.UserDescription = "دوست دارم کیف پولم رو شارژ کنم";

		request.WalletToken = System.Guid.NewGuid();
		request.CompanyToken = Data.Configurations.SeedData.Constant.Token.Company;

		var result =
			usersController.Deposite(request: request) as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as Dtat.Result;

		Assert.NotNull(@object: result);

		Assert.False(condition: value!.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.ThereIsNotAnyItemWithThisToken,
			arg0: "company");

		Assert.Equal(expected: errorMessage, actual: value.ErrorMessages[0]);
	}
}
