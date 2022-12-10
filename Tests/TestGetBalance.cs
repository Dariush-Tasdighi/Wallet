using Microsoft.EntityFrameworkCore;

namespace Tests;

public class TestGetBalance
{
	public TestGetBalance() : base()
	{
		var options =
			new DbContextOptionsBuilder<Data.DatabaseContext>()
			.UseInMemoryDatabase(databaseName: "UsersControllerTest")
			.ConfigureWarnings(current => current.Ignore
			(Microsoft.EntityFrameworkCore.Diagnostics.InMemoryEventId.TransactionIgnoredWarning))
			.Options;

		DatabaseContext =
			new Data.DatabaseContext(options: options);

		DatabaseContext.Database.EnsureDeleted();
		DatabaseContext.Database.EnsureCreated();
	}

	public Data.DatabaseContext DatabaseContext { get; }

	[Fact]
	public void TestServerIP()
	{
		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: null);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var response =
			usersController.GetBalance(request: null);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.TheItemIsNull,
			arg0: "serverIP");

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestRequestIsNotNull()
	{
		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: "192.168.1.110");
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var response =
			usersController.GetBalance(request: null);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.TheItemIsNull,
			arg0: "request");

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestCompanyExists()
	{
		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: "192.168.1.110");
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto();

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.Company>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.ThereIsNotAnyItemWithThisToken,
			arg0: "company");

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestCompanyIsActive()
	{
		// **************************************************
		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = false,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: "192.168.1.110");
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				CompanyToken = companyToken,
			};

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.Company>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.TheItemIsNotActive,
			arg0: "company");

		Assert.Equal(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestCompanyServerIP()
	{
		// **************************************************
		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		var serverIP =
			"192.168.1.110";

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				CompanyToken = companyToken,
			};

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as Dtat.Result;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.ThisIPIsNotDefinedForThisCompany,
			arg0: serverIP);

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestCompanyServerIPIsActive()
	{
		// **************************************************
		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = false,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				CompanyToken = companyToken,
			};

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as Dtat.Result;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.ThisIPIsNotActive,
			arg0: serverIP);

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestWallet()
	{
		// **************************************************
		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				CompanyToken = companyToken,
			};

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.Wallet>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.ThereIsNotAnyItemWithThisToken,
			arg0: "wallet");

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestWalletIsActive()
	{
		// **************************************************
		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = false,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.Wallet>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.TheItemIsNotActive,
			arg0: "wallet");

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestCompanyWallet()
	{
		// **************************************************
		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.CompanyWallet>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.TheCompanyDoesNotHaveAccessToThisWallet;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestCompanyWalletIsActive()
	{
		// **************************************************
		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = false,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.CompanyWallet>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.TheCompanyAccessToThisWalletIsNotActive;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUser()
	{
		// **************************************************
		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = "09123456789";

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.User>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.NoUserWithThisCellPhoneNumber;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUserDataConsistency()
	{
		// **************************************************
		var cellPhoneNumber = "09123456789";

		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();

		var user =
			new Domain.User(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
			{
				IsActive = false,
				NationalCode = "1234567890",
				EmailAddress = "AliRezaAlavi@Gmail.com",
			};

		user.UpdateHash();

		user.IsVerified = true;

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = cellPhoneNumber;

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.User>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.InconsitencyDataForUser;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUserIsActive()
	{
		// **************************************************
		var cellPhoneNumber = "09123456789";

		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();

		var user =
			new Domain.User(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
			{
				IsActive = false,
				NationalCode = "1234567890",
				EmailAddress = "AliRezaAlavi@Gmail.com",
			};

		user.UpdateHash();

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = cellPhoneNumber;

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.User>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage = string.Format
			(format: Resources.Messages.Errors.TheItemIsNotActive,
			arg0: "user");

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUserWallet()
	{
		// **************************************************
		var cellPhoneNumber = "09123456789";

		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();

		var user =
			new Domain.User(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
			{
				IsActive = true,
				NationalCode = "1234567890",
				EmailAddress = "AliRezaAlavi@Gmail.com",
			};

		user.UpdateHash();

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = cellPhoneNumber;

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.UserWallet>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.TheUserDoesNotHaveAccessToThisWallet;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUserWalletDataConsistency()
	{
		// **************************************************
		var cellPhoneNumber = "09123456789";

		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();

		var user =
			new Domain.User(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
			{
				IsActive = true,
				NationalCode = "1234567890",
				EmailAddress = "AliRezaAlavi@Gmail.com",
			};

		user.UpdateHash();

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();

		var userWallet =
			new Domain.UserWallet(userId: user.Id, walletId: wallet.Id)
			{
				Balance = 0,
				IsActive = false,
			};

		userWallet.UpdateHash();

		userWallet.Balance = 100;

		DatabaseContext.Add(entity: userWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = cellPhoneNumber;

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.UserWallet>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.InconsitencyDataForUserWallet;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUserWalletIsActive()
	{
		// **************************************************
		var cellPhoneNumber = "09123456789";

		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();

		var user =
			new Domain.User(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
			{
				IsActive = true,
				NationalCode = "1234567890",
				EmailAddress = "AliRezaAlavi@Gmail.com",
			};

		user.UpdateHash();

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();

		var userWallet =
			new Domain.UserWallet(userId: user.Id, walletId: wallet.Id)
			{
				Balance = 0,
				IsActive = false,
			};

		userWallet.UpdateHash();

		DatabaseContext.Add(entity: userWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = cellPhoneNumber;

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Domain.UserWallet>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.TheUserAccessToThisWalletIsNotActive;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUserBalanceWithCheckingDataConsistency()
	{
		// **************************************************
		var cellPhoneNumber = "09123456789";

		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();

		var companyWallet =
			new Domain.CompanyWallet(companyId: company.Id, walletId: wallet.Id)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();

		var serverIP =
			"192.168.1.110";

		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();

		var user = new Domain.User
			(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
		{
			IsActive = true,
			NationalCode = "1234567890",
			EmailAddress = "AliRezaAlavi@Gmail.com",
		};

		user.UpdateHash();

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();

		var userWallet = new Domain.UserWallet
			(userId: user.Id, walletId: wallet.Id)
		{
			Balance = 100,
			IsActive = true,
		};

		userWallet.UpdateHash();

		DatabaseContext.Add(entity: userWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = cellPhoneNumber;

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<decimal>;

		Assert.NotNull(@object: value);

		Assert.False(condition: value.IsSuccess);

		Assert.Equal(expected: 1, actual: value.ErrorMessages.Count);

		var errorMessage =
			Resources.Messages.Errors.InconsitencyDataForUserBalance;

		Assert.Equal
			(expected: errorMessage, actual: value.ErrorMessages[0]);
	}

	[Fact]
	public void TestUserBalance()
	{
		// **************************************************
		var userIP =
			"192.168.1.100";

		var serverIP =
			"192.168.1.110";

		var cellPhoneNumber = "09123456789";

		var walletToken =
			new System.Guid(g: "55308747-A312-4171-9025-901B3AF4F435");

		var companyToken =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var wallet =
			new Domain.Wallet(name: "Some Wallet")
			{
				IsActive = true,
			};

		wallet.UpdateToken(token: walletToken);

		DatabaseContext.Add(entity: wallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var company =
			new Domain.Company(name: "Some Place")
			{
				IsActive = true,
			};

		company.UpdateToken(token: companyToken);

		DatabaseContext.Add(entity: company);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var companyWallet = new Domain.CompanyWallet
			(companyId: company.Id, walletId: wallet.Id)
		{
			IsActive = true,
		};

		DatabaseContext.Add(entity: companyWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var validIP =
			new Domain.ValidIP
			(companyId: company.Id, serverIP: serverIP)
			{
				IsActive = true,
			};

		DatabaseContext.Add(entity: validIP);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var user = new Domain.User
			(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
		{
			IsActive = true,
			NationalCode = "1234567890",
			EmailAddress = "AliRezaAlavi@Gmail.com",
		};

		user.UpdateHash();

		DatabaseContext.Add(entity: user);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		var userWallet = new Domain.UserWallet
			(userId: user.Id, walletId: wallet.Id)
		{
			Balance = 250,
			IsActive = true,
		};

		userWallet.UpdateHash();

		DatabaseContext.Add(entity: userWallet);

		DatabaseContext.SaveChanges();
		// **************************************************

		// **************************************************
		Domain.Transaction transaction;

		// (1)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: 10, serverIP: serverIP, userIP: userIP)
			{
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500001",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,

				WithdrawDateTime =
					new System.DateTime
					(year: 2022, month: 1, day: 1,
					hour: 1, minute: 0, second: 0),
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (2)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: -5, serverIP: serverIP, userIP: userIP)
			{
				WithdrawDateTime = null,
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500002",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Payment,
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (3)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: 20, serverIP: serverIP, userIP: userIP)
			{
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500003",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,

				WithdrawDateTime =
					new System.DateTime
					(year: 2022, month: 1, day: 3,
					hour: 1, minute: 0, second: 0),
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (4)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: -10, serverIP: serverIP, userIP: userIP)
			{
				WithdrawDateTime = null,
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500004",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Payment,
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (5)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: 30, serverIP: serverIP, userIP: userIP)
			{
				WithdrawDateTime = null,
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500005",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (6)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: 40, serverIP: serverIP, userIP: userIP)
			{
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500006",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,

				WithdrawDateTime =
					new System.DateTime
					(year: 2022, month: 1, day: 5,
					hour: 1, minute: 0, second: 0),
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (7)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: -15, serverIP: serverIP, userIP: userIP)
			{
				WithdrawDateTime = null,
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500007",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Payment,
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (8)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: 50, serverIP: serverIP, userIP: userIP)
			{
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500008",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,

				WithdrawDateTime =
					new System.DateTime
					(year: 2022, month: 1, day: 7,
					hour: 1, minute: 0, second: 0),
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (9)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: 60, serverIP: serverIP, userIP: userIP)
			{
				WithdrawDateTime = null,
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500009",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		// (10)
		transaction =
			new Domain.Transaction
			(userId: user.Id, walletId: wallet.Id,
			amount: 70, serverIP: serverIP, userIP: userIP)
			{
				DepositeOrWithdrawProviderName = "Iran Kish",
				DepositeOrWithdrawReferenceCode = "1234500010",
				Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,

				WithdrawDateTime =
					new System.DateTime
					(year: 2022, month: 1, day: 9,
					hour: 1, minute: 0, second: 0),
			};

		transaction.UpdateHash();

		DatabaseContext.Add(entity: transaction);

		DatabaseContext.SaveChanges();
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		var mockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<Server.Controllers.UsersController>>();
		// **************************************************

		// **************************************************
		var mockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		mockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: serverIP);
		// **************************************************

		var request =
			new Dtos.Users.GetBalanceRequestDto()
			{
				WalletToken = walletToken,
				CompanyToken = companyToken,
			};

		request.User.CellPhoneNumber = cellPhoneNumber;

		// **************************************************
		// **************************************************
		// **************************************************
		var usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		var response =
			usersController.GetBalance(request: request);
		// **************************************************

		// **************************************************
		Assert.NotNull(@object: response);

		var result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		var value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: value);

		Assert.True(condition: value.IsSuccess);

		Assert.Equal(expected: 0, actual: value.ErrorMessages.Count);

		Assert.NotNull(@object: value.Data);

		Assert.Equal
			(expected: 250, actual: value.Data.Balance);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		var now =
			new System.DateTime
			(year: 2022, month: 1, day: 6,
			hour: 0, minute: 0, second: 0);

		mockUtility.Setup(current => current
			.GetNow())
			.Returns(now);

		usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		response =
			usersController.GetBalance(request: request);
		// **************************************************

		// **************************************************
		Assert.NotNull(@object: response);

		result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: value);

		Assert.True(condition: value.IsSuccess);

		Assert.Equal(expected: 0, actual: value.ErrorMessages.Count);

		Assert.NotNull(@object: value.Data);

		Assert.Equal
			(expected: 250, actual: value.Data.Balance);

		Assert.Equal
			(expected: 40, actual: value.Data.WithdrawBalance);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		now =
			new System.DateTime
			(year: 2022, month: 1, day: 8,
			hour: 0, minute: 0, second: 0);

		mockUtility.Setup(current => current
			.GetNow())
			.Returns(now);

		usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		response =
			usersController.GetBalance(request: request);
		// **************************************************

		// **************************************************
		Assert.NotNull(@object: response);

		result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: value);

		Assert.True(condition: value.IsSuccess);

		Assert.Equal(expected: 0, actual: value.ErrorMessages.Count);

		Assert.NotNull(@object: value.Data);

		Assert.Equal
			(expected: 250, actual: value.Data.Balance);

		Assert.Equal
			(expected: 90, actual: value.Data.WithdrawBalance);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		now =
			new System.DateTime
			(year: 2022, month: 1, day: 10,
			hour: 0, minute: 0, second: 0);

		mockUtility.Setup(current => current
			.GetNow())
			.Returns(now);

		usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		response =
			usersController.GetBalance(request: request);
		// **************************************************

		// **************************************************
		Assert.NotNull(@object: response);

		result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: value);

		Assert.True(condition: value.IsSuccess);

		Assert.Equal(expected: 0, actual: value.ErrorMessages.Count);

		Assert.NotNull(@object: value.Data);

		Assert.Equal
			(expected: 250, actual: value.Data.Balance);

		Assert.Equal
			(expected: 160, actual: value.Data.WithdrawBalance);
		// **************************************************
		// **************************************************
		// **************************************************

		// **************************************************
		// **************************************************
		// **************************************************
		now =
			new System.DateTime
			(year: 2022, month: 1, day: 9,
			hour: 0, minute: 0, second: 0);

		mockUtility.Setup(current => current
			.GetNow())
			.Returns(now);

		usersController =
			new Server.Controllers.UsersController(logger: mockLogger.Object,
			databaseContext: DatabaseContext, utility: mockUtility.Object);

		response =
			usersController.GetBalance(request: request);
		// **************************************************

		// **************************************************
		Assert.NotNull(@object: response);

		result =
			response.Result as
			Microsoft.AspNetCore.Mvc.OkObjectResult;

		Assert.NotNull(@object: result);

		value =
			result.Value as
			Dtat.Result<Dtos.Users.GetBalanceResponseDto>;

		Assert.NotNull(@object: value);

		Assert.True(condition: value.IsSuccess);

		Assert.Equal(expected: 0, actual: value.ErrorMessages.Count);

		Assert.NotNull(@object: value.Data);

		Assert.Equal
			(expected: 250, actual: value.Data.Balance);

		Assert.Equal
			(expected: 160, actual: value.Data.WithdrawBalance);
		// **************************************************
		// **************************************************
		// **************************************************
	}
}
