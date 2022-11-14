using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

public class UsersController : Infrastructure.ControllerBaseWithDatabaseContext
{
	private static object Locker = new();

	#region Constructor
	public UsersController
		(ILogger<UsersController> logger,
		Data.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}
	#endregion /Constructor

	#region Properties
	private ILogger<UsersController> Logger { get; }
	#endregion /Properties

	#region GetBalanceAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet
		(template: "[action]/{waletToken}/{companyUserIdentity}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Wallet),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<decimal>>
		GetBalanceAsync(System.Guid waletToken, string cellPhoneNumber)
	{
		try
		{
			var item =
				await
				DatabaseContext.UserWallets
				.AsNoTracking()
				.Where(current => current.IsActive)

				.Where(current => current.Wallet != null && current.Wallet.IsActive)
				.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)

				.Where(current => current.User != null && current.User.IsActive)
				.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)

				.FirstOrDefaultAsync();

			if (item == null)
			{
				return NotFound(value: 0);
			}

			return Ok(value: item.Balance);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_GetBalanceAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetBalanceAsync()

	#region GetLastTransactionsAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet
		(template: "[action]/{waletToken}/{companyUserIdentity}/{count}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Wallet),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IList<Domain.Transaction>>>
		GetLastTransactionsAsync(System.Guid waletToken, string cellPhoneNumber, int count)
	{
		try
		{
			//var foundedUserWallet =
			//	await
			//	DatabaseContext.UserWallets
			//	.AsNoTracking()
			//	.Where(current => current.IsActive)
			//	.Where(current => current.User != null && current.User.IsActive)
			//	.Where(current => current.Wallet != null && current.Wallet.IsActive)
			//	.Where(current => current.CompanyUserIdentity == companyUserIdentity)
			//	.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)
			//	.FirstOrDefaultAsync();

			// فارغ از هر شرایطی، امکان نمایش تراکنش‌های کاربر
			// بر روی کیف پول باید امکان‌پذیر باشد
			var foundedUserWallet =
				await
				DatabaseContext.UserWallets
				.AsNoTracking()
				.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)
				.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
				.FirstOrDefaultAsync();

			if (foundedUserWallet == null)
			{
				return NotFound(value: null);
			}

			var transactions =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.UserId == foundedUserWallet.UserId)
				.Where(current => current.WalletId == foundedUserWallet.WalletId)
				.Skip(count: 0)
				.Take(count: count)
				.ToListAsync()
				;

			return Ok(value: transactions);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_GetLastTransactionsAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetLastTransactions()

	//#region RegisterUserAsync()
	//[Microsoft.AspNetCore.Mvc.HttpGet
	//	(template: "[action]/{waletToken}/{companyUserIdentity}/{count}")]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(Domain.Wallet),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	//public async System.Threading.Tasks.Task
	//	<Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IList<Domain.Transaction>>>
	//	RegisterUserAsync(System.Guid waletToken, string companyUserIdentity, int count)
	//{
	//	try
	//	{
	//		//var foundedUserWallet =
	//		//	await
	//		//	DatabaseContext.UserWallets
	//		//	.AsNoTracking()
	//		//	.Where(current => current.IsActive)
	//		//	.Where(current => current.User != null && current.User.IsActive)
	//		//	.Where(current => current.Wallet != null && current.Wallet.IsActive)
	//		//	.Where(current => current.CompanyUserIdentity == companyUserIdentity)
	//		//	.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)
	//		//	.FirstOrDefaultAsync();

	//		// فارغ از هر شرایطی، امکان نمایش تراکنش‌های کاربر
	//		// بر روی کیف پول باید امکان‌پذیر باشد
	//		var foundedUserWallet =
	//			await
	//			DatabaseContext.UserWallets
	//			.AsNoTracking()
	//			.Where(current => current.CompanyUserIdentity == companyUserIdentity)
	//			.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)
	//			.FirstOrDefaultAsync();

	//		if (foundedUserWallet == null)
	//		{
	//			return NotFound(value: null);
	//		}

	//		var transactions =
	//			await
	//			DatabaseContext.Transactions
	//			.AsNoTracking()
	//			.Where(current => current.UserId == foundedUserWallet.UserId)
	//			.Where(current => current.WalletId == foundedUserWallet.WalletId)
	//			.Skip(count: 0)
	//			.Take(count: count)
	//			.ToListAsync()
	//			;

	//		return Ok(value: transactions);
	//	}
	//	catch (System.Exception ex)
	//	{
	//		var applicationError =
	//			new Infrastructure.ApplicationError
	//			(code: Infrastructure.Constant.ErrorCode.Root_UsersController_GetLastTransactionsAsync,
	//			message: ex.Message, innerException: ex);

	//		Logger.LogError
	//			(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

	//		return StatusCode(statusCode: Microsoft.AspNetCore
	//			.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
	//	}
	//}
	//#endregion /GetLastTransactions()

	#region Deposite()
	/// <summary>
	/// تعریف نمی‌کنیم Async به دلیل مسائل امنیتی و هم‌زمانی این تابع را
	/// </summary>
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(long),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public Microsoft.AspNetCore.Mvc.ActionResult Deposite(Dtos.Users.DepositeRequestDto request)
	{
		try
		{
			ValidateDepositeRequest(request: request);

			lock (Locker)
			{
				var wallet =
					DatabaseContext.Wallets
					.Where(current => current.Token == request.WaletToken)
					.FirstOrDefault();

				if (wallet == null)
				{
					return NotFound(value: "Wallet not found!");
				}

				if (wallet.IsActive)
				{
					return NotFound(value: "Wallet is not active!");
				}

				var user =
					CreateOrUpdateUser
					(cellPhoneNumber: request.User.CellPhoneNumber!,
					displayName: request.User.DisplayName!,

					emailAddress: request.User.EmailAddress,
					nationalCode: request.User.NationalCode,
					additionalData: request.User.AdditionalData);

				var userWallet =
					CreateOrUpdateUserWallet
					(userId: user.Id, walletId: wallet.Id,
					paymentFeatureIsEnabled: request.User.PaymentFeatureIsEnabled,
					depositeFeatureIsEnabled: request.User.DepositeFeatureIsEnabled,
					withdrawFeatureIsEnabled: request.User.WithdrawFeatureIsEnabled);

				// TODO
			}

			// TODO
			return Ok(value: null);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_Deposite,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Deposite()

	private void ValidateDepositeRequest(Dtos.Users.DepositeRequestDto request)
	{
		if (request.User == null)
		{
			throw new System
				.ArgumentNullException(paramName: nameof(request.User));
		}

		// TODO
	}

	private Domain.User CreateOrUpdateUser
		(string cellPhoneNumber, string displayName,
		string? emailAddress, string? nationalCode, string? additionalData)
	{
		var user =
			DatabaseContext.Users
			.Where(current => current.CellPhoneNumber == cellPhoneNumber)
			.FirstOrDefault();

		if (user == null)
		{
			user =
				new Domain.User(cellPhoneNumber: cellPhoneNumber, displayName: displayName)
				{
					//Id
					//Hash
					//IsActive
					//Description
					//UserWallets
					//Transactions
					//InsertDateTime
					//UpdateDateTime
					//CellPhoneNumber

					DisplayName = displayName,
					NationalCode = nationalCode,
					EmailAddress = emailAddress,
					AdditionalData = additionalData,
				};

			DatabaseContext.Add(entity: user);

			// دستور ذیل باید نوشته شود Id به دلیل نوع
			DatabaseContext.SaveChanges();
		}
		else
		{
			user.DisplayName = displayName;
			user.NationalCode = nationalCode;
			user.EmailAddress = emailAddress;
			user.AdditionalData = additionalData;
		}

		return user;
	}

	private Domain.UserWallet CreateOrUpdateUserWallet
		(long userId, long walletId,
		bool paymentFeatureIsEnabled,
		bool depositeFeatureIsEnabled,
		bool withdrawFeatureIsEnabled)
	{
		var userWallet =
			DatabaseContext.UserWallets
			.Where(current => current.UserId == userId)
			.Where(current => current.WalletId == walletId)
			.FirstOrDefault();

		if (userWallet == null)
		{
			userWallet =
				new Domain.UserWallet(userId: userId, walletId: walletId)
				{
					//Id
					//Hash
					//User
					//UserId
					//Wallet
					//WalletId
					//IsActive
					//Description
					//AdditionalData
					//InsertDateTime
					//UpdateDateTime

					Balance = 0,

					PaymentFeatureIsEnabled = paymentFeatureIsEnabled,
					DepositeFeatureIsEnabled = depositeFeatureIsEnabled,
					WithdrawFeatureIsEnabled = withdrawFeatureIsEnabled,
				};

			DatabaseContext.Add(entity: userWallet);

			// دستور ذیل باید نوشته شود Id به دلیل نوع
			DatabaseContext.SaveChanges();
		}
		else
		{
			userWallet.PaymentFeatureIsEnabled = paymentFeatureIsEnabled;
			userWallet.DepositeFeatureIsEnabled = depositeFeatureIsEnabled;
			userWallet.WithdrawFeatureIsEnabled = withdrawFeatureIsEnabled;
		}

		return userWallet;
	}
}
