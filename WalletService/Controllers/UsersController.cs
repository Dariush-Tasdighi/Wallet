using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Azure.Core;

namespace Server.Controllers;

public class UsersController :
	Infrastructure.ControllerBaseWithDatabaseContext
{
	private static object Locker = new();

	#region Constructor
	public UsersController
		(ILogger<UsersController> logger,
		Data.DatabaseContext databaseContext) :
		base(databaseContext: databaseContext)
	{
		Logger = logger;
	}
	#endregion /Constructor

	#region Properties
	private ILogger<UsersController> Logger { get; }
	#endregion /Properties

	#region Action: GetBalance()
	[Microsoft.AspNetCore.Mvc.HttpPost
		(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtat.Result<decimal>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public Microsoft.AspNetCore.Mvc.ActionResult
		GetBalance(Dtos.Users.GetBalanceRequestDto request)
	{
		try
		{
			var result =
				new Dtat.Result<decimal>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Infrastructure.Utility
				.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage =
					$"Server IP is null!";

				result.AddErrorMessages
					(message: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			// بررسی معتبر بودن فیلدهای ارسال شده
			// **************************************************
			var validateEntityResult =
				Dtat.Utility.ValidateEntity(entity: request);

			if (validateEntityResult.IsSuccess == false)
			{
				return Ok(value: validateEntityResult);
			}
			// **************************************************

			lock (Locker)
			{
				// **************************************************
				// بررسی شرکت بر اساس توکن
				// **************************************************
				var companyResult =
					Services.CompaniesService.CheckAndGetCompanyByToken
					(databaseContext: DatabaseContext, token: request.CompanyToken);

				if (companyResult.IsSuccess == false)
				{
					return Ok(value: companyResult);
				}

				var company =
					companyResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (company == null)
				{
					var errorMessage =
						$"${nameof(Domain.Company)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی مجاز بودن آی‌پی سرور درخواست کننده
				// **************************************************
				var validIPResult =
					Services.ValidIPsService.CheckServerIPByCompanyToken
					(databaseContext: DatabaseContext, serverIP: serverIP,
					companyToken: request.CompanyToken, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber);

				if (validIPResult.IsSuccess == false)
				{
					return Ok(value: validIPResult);
				}
				// **************************************************

				// **************************************************
				// بررسی کیف پول بر اساس توکن
				// **************************************************
				var walletResult =
					Services.WalletsService.CheckAndGetWalletByToken
					(databaseContext: DatabaseContext, token: request.WalletToken);

				if (walletResult.IsSuccess == false)
				{
					return Ok(value: companyResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.Wallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی شرکت به کیف پول مربوطه، بر اساس  توکن‌های آن‌ها
				// **************************************************
				var companyWalletResult =
					Services.CompanyWalletsService.CheckAndGetCompanyWalletByTokens
					(databaseContext: DatabaseContext, companyToken: request.CompanyToken, walletToken: request.WalletToken);

				if (companyWalletResult.IsSuccess == false)
				{
					return Ok(value: companyWalletResult);
				}

				var companyWallet =
					companyWalletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (companyWallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.CompanyWallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی کاربر
				// **************************************************
				var userResult =
					Services.UsersService.CheckAndGetUserByCellPhoneNumber
					(databaseContext: DatabaseContext,
					cellPhoneNumber: request.User.CellPhoneNumber);

				if (userResult.IsSuccess == false)
				{
					return Ok(value: userResult);
				}

				var user =
					userResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (user == null)
				{
					var errorMessage =
						$"${nameof(Domain.User)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی کاربر به کیف پول مربوطه
				// **************************************************
				var userWalletResult =
					Services.UserWalletsService.CheckAndGetUserWallet(databaseContext: DatabaseContext,
					cellPhoneNumber: request.User.CellPhoneNumber, walletToken: request.WalletToken);

				if (userWalletResult.IsSuccess == false)
				{
					return Ok(value: userWalletResult);
				}

				var userWallet =
					userWalletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (userWallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.UserWallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بدست آوردن مانده کیف پول کاربر
				// با احتساب چک کردن معتبر بودن آن
				// **************************************************
				var balanceResult =
					Services.UserWalletsService.GetUserBalanceWithCheckingDataConsistency
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, userWallet: userWallet);

				if (balanceResult.IsSuccess == false)
				{
					return Ok(value: balanceResult);
				}
				// **************************************************

				// **************************************************
				result.Data =
					balanceResult.Data;

				return Ok(value: result);
				// **************************************************
			}
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_GetBalance,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Action: GetBalance()

	//#region Action: GetLastTransactionsAsync()
	//[Microsoft.AspNetCore.Mvc.HttpGet
	//	(template: "[action]/{walletToken}/{cellPhoneNumber}/{count}")]

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
	//	GetLastTransactionsAsync(System.Guid walletToken, string cellPhoneNumber, int count)
	//{
	//	try
	//	{
	//		//var foundedUserWallet =
	//		//	await
	//		//	DatabaseContext.UserWallets
	//		//	.AsNoTracking()
	//		//	.Where(current => current.IsActive)

	//		//	.Where(current => current.Wallet != null && current.Wallet.IsActive)
	//		//	.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)

	//		//	.Where(current => current.User != null && current.User.IsActive)
	//		//	.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
	//		//	.FirstOrDefaultAsync();

	//		// فارغ از هر شرایطی، امکان نمایش تراکنش‌های کاربر
	//		// بر روی کیف پول باید امکان‌پذیر باشد
	//		var foundedUserWallet =
	//			await
	//			DatabaseContext.UserWallets
	//			.AsNoTracking()
	//			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
	//			.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
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
	//#endregion /Action: GetLastTransactions()

	#region Action: Deposite()
	/// <summary>
	/// تعریف نمی‌کنیم Async به دلیل مسائل امنیتی و هم‌زمانی این تابع را
	/// </summary>
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtat.Result<Dtos.Users.DepositeResponseDto>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public Microsoft.AspNetCore.Mvc.ActionResult Deposite(Dtos.Users.DepositeRequestDto request)
	{
		try
		{
			var startTime =
				System.DateTime.Now;

			var result = new Dtat.Result
				<Dtos.Users.DepositeResponseDto>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Infrastructure.Utility
				.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage =
					$"Server IP is null!";

				result.AddErrorMessages
					(message: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			// بررسی معتبر بودن فیلدهای ارسال شده
			// **************************************************
			var validateEntityResult =
				Dtat.Utility.ValidateEntity(entity: request);

			if (validateEntityResult.IsSuccess == false)
			{
				return Ok(value: validateEntityResult);
			}
			// **************************************************

			lock (Locker)
			{
				// **************************************************
				// بررسی شرکت بر اساس توکن
				// **************************************************
				var companyResult =
					Services.CompaniesService.CheckAndGetCompanyByToken
					(databaseContext: DatabaseContext, token: request.CompanyToken);

				if (companyResult.IsSuccess == false)
				{
					return Ok(value: companyResult);
				}

				var company =
					companyResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (company == null)
				{
					var errorMessage =
						$"${nameof(Domain.Company)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی مجاز بودن آی‌پی سرور درخواست کننده
				// **************************************************
				var validIPResult =
					Services.ValidIPsService.CheckServerIPByCompanyToken
					(databaseContext: DatabaseContext, serverIP: serverIP,
					companyToken: request.CompanyToken, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber);

				if (validIPResult.IsSuccess == false)
				{
					return Ok(value: validIPResult);
				}
				// **************************************************

				// **************************************************
				// بررسی کیف پول بر اساس توکن
				// **************************************************
				var walletResult =
					Services.WalletsService.CheckAndGetWalletByToken
					(databaseContext: DatabaseContext, token: request.WalletToken);

				if (walletResult.IsSuccess == false)
				{
					return Ok(value: companyResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.Wallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				if (wallet.DepositeFeatureIsEnabled == false)
				{
					var errorMessage =
						$"Deposite feature is not enabled for this wallet!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی شرکت به کیف پول مربوطه، بر اساس  توکن‌های آن‌ها
				// **************************************************
				var companyWalletResult =
					Services.CompanyWalletsService.CheckAndGetCompanyWalletByTokens
					(databaseContext: DatabaseContext, companyToken: request.CompanyToken, walletToken: request.WalletToken);

				if (companyWalletResult.IsSuccess == false)
				{
					return Ok(value: companyWalletResult);
				}

				var companyWallet =
					companyWalletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (companyWallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.CompanyWallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی کاربر
				// **************************************************
				var user =
					Services.UsersService.CreateOrUpdateUser
					(databaseContext: DatabaseContext,

					displayName: request.User.DisplayName,
					cellPhoneNumber: request.User.CellPhoneNumber,

					emailAddress: request.User.EmailAddress,
					nationalCode: request.User.NationalCode);

				if (user.IsActive == false)
				{
					var errorMessage =
						$"User is not active!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی کاربر به کیف پول مربوطه
				// **************************************************
				var userWallet =
					Services.UserWalletsService.CreateOrUpdateUserWallet
					(databaseContext: DatabaseContext,
					userId: user.Id, walletId: wallet.Id,
					additionalData: request.AdditionalData,
					paymentFeatureIsEnabled: request.User.PaymentFeatureIsEnabled,
					depositeFeatureIsEnabled: request.User.DepositeFeatureIsEnabled,
					withdrawFeatureIsEnabled: request.User.WithdrawFeatureIsEnabled,
					transferFeatureIsEnabled: request.User.TransferFeatureIsEnabled);

				if (userWallet.IsActive == false)
				{
					var errorMessage =
						$"User is not active in this wallet!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				if (userWallet.DepositeFeatureIsEnabled == false)
				{
					var errorMessage =
						$"Deposite feature is not enabled for this user!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بدست آوردن مانده کیف پول کاربر
				// با احتساب چک کردن معتبر بودن آن
				// **************************************************
				var balanceResult =
					Services.UserWalletsService.GetUserBalanceWithCheckingDataConsistency
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, userWallet: userWallet);

				if (balanceResult.IsSuccess == false)
				{
					return Ok(value: balanceResult);
				}
				// **************************************************

				// **************************************************
				// افزایش مانده حساب کاربر در کیف پول جاری
				// **************************************************
				userWallet.Balance += request.Amount;

				userWallet.UpdateHash();
				// **************************************************

				// **************************************************
				// محاسبه کل زمان پردازش
				// **************************************************
				var finishTime =
					System.DateTime.Now;

				var transactionDuration = finishTime - startTime;
				// **************************************************

				// **************************************************
				var transaction =
					new Domain.Transaction
					(userId: user.Id, walletId: wallet.Id,
					amount: request.Amount, userIP: request.User.IP, serverIP: serverIP)
					{
						ServerIP = serverIP,
						UserIP = request.User.IP,

						TransactionDuration = transactionDuration,

						AdditionalData = request.AdditionalData,
						UserDescription = request.UserDescription,
						SystemicDescription = request.SystemicDescription,

						DepositeOrWithdrawProviderName = request.ProviderName,
						DepositeOrWithdrawReferenceCode = request.ReferenceCode,
					};

				transaction.UpdateHash();

				DatabaseContext.Add(entity: transaction);
				// **************************************************

				// **************************************************
				// ذخیره تغییرات در بانک اطلاعاتی
				// **************************************************
				DatabaseContext.SaveChanges();
				// **************************************************

				// **************************************************
				var depositeResponseDto =
					new Dtos.Users.DepositeResponseDto
					(balance: userWallet.Balance, transactionId: transaction.Id);

				result.Data =
					depositeResponseDto;

				return Ok(value: result);
				// **************************************************
			}
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
	#endregion /Action: Deposite()

	#region Action: Payment()
	/// <summary>
	/// تعریف نمی‌کنیم Async به دلیل مسائل امنیتی و هم‌زمانی این تابع را
	/// </summary>
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtat.Result<Dtos.Users.PaymentResponseDto>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public Microsoft.AspNetCore.Mvc.ActionResult Payment(Dtos.Users.PaymentRequestDto request)
	{
		try
		{
			var startTime =
				System.DateTime.Now;

			var result = new Dtat.Result
				<Dtos.Users.PaymentResponseDto>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Infrastructure.Utility
				.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage =
					$"Server IP is null!";

				result.AddErrorMessages
					(message: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			// بررسی معتبر بودن فیلدهای ارسال شده
			// **************************************************
			var validateEntityResult =
				Dtat.Utility.ValidateEntity(entity: request);

			if (validateEntityResult.IsSuccess == false)
			{
				return Ok(value: validateEntityResult);
			}
			// **************************************************

			lock (Locker)
			{
				// **************************************************
				// بررسی شرکت بر اساس توکن
				// **************************************************
				var companyResult =
					Services.CompaniesService.CheckAndGetCompanyByToken
					(databaseContext: DatabaseContext, token: request.CompanyToken);

				if (companyResult.IsSuccess == false)
				{
					return Ok(value: companyResult);
				}

				var company =
					companyResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (company == null)
				{
					var errorMessage =
						$"${nameof(Domain.Company)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی مجاز بودن آی‌پی سرور درخواست کننده
				// **************************************************
				var validIPResult =
					Services.ValidIPsService.CheckServerIPByCompanyToken
					(databaseContext: DatabaseContext, serverIP: serverIP,
					companyToken: request.CompanyToken, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber);

				if (validIPResult.IsSuccess == false)
				{
					return Ok(value: validIPResult);
				}
				// **************************************************

				// **************************************************
				// بررسی کیف پول بر اساس توکن
				// **************************************************
				var walletResult =
					Services.WalletsService.CheckAndGetWalletByToken
					(databaseContext: DatabaseContext, token: request.WalletToken);

				if (walletResult.IsSuccess == false)
				{
					return Ok(value: companyResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.Wallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				if (wallet.PaymentFeatureIsEnabled == false)
				{
					var errorMessage =
						$"Payment feature is not enabled for this wallet!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی شرکت به کیف پول مربوطه، بر اساس  توکن‌های آن‌ها
				// **************************************************
				var companyWalletResult =
					Services.CompanyWalletsService.CheckAndGetCompanyWalletByTokens
					(databaseContext: DatabaseContext, companyToken: request.CompanyToken, walletToken: request.WalletToken);

				if (companyWalletResult.IsSuccess == false)
				{
					return Ok(value: companyWalletResult);
				}

				var companyWallet =
					companyWalletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (companyWallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.CompanyWallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی کاربر
				// **************************************************
				var userResult =
					Services.UsersService.CheckAndGetUserByCellPhoneNumber
					(databaseContext: DatabaseContext,
					cellPhoneNumber: request.User.CellPhoneNumber);

				if (userResult.IsSuccess == false)
				{
					return Ok(value: userResult);
				}

				var user =
					userResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (user == null)
				{
					var errorMessage =
						$"${nameof(Domain.User)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی کاربر به کیف پول مربوطه
				// **************************************************
				var userWalletResult =
					Services.UserWalletsService.CheckAndGetUserWallet(databaseContext: DatabaseContext,
					cellPhoneNumber: request.User.CellPhoneNumber, walletToken: request.WalletToken);

				if (userWalletResult.IsSuccess == false)
				{
					return Ok(value: userWalletResult);
				}

				var userWallet =
					userWalletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (userWallet == null)
				{
					var errorMessage =
						$"${nameof(Domain.UserWallet)} is null!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				if (userWallet.PaymentFeatureIsEnabled == false)
				{
					var errorMessage =
						$"Payment feature is not enabled for this user!";

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بدست آوردن مانده کیف پول کاربر
				// با احتساب چک کردن معتبر بودن آن
				// **************************************************
				var balanceResult =
					Services.UserWalletsService.GetUserBalanceWithCheckingDataConsistency
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, userWallet: userWallet);

				if (balanceResult.IsSuccess == false)
				{
					return Ok(value: balanceResult);
				}
				// **************************************************

				// **************************************************
				// کاهش مانده حساب کاربر در کیف پول جاری
				// **************************************************
				if (request.Amount > userWallet.Balance)
				{
					var errorMessage =
						$"The amount value is more than user balance value!";

					result.AddErrorMessages(message: errorMessage);

					return Ok(value: result);
				}

				userWallet.Balance -= request.Amount;

				userWallet.UpdateHash();
				// **************************************************

				// **************************************************
				// محاسبه کل زمان پردازش
				// **************************************************
				var finishTime =
					System.DateTime.Now;

				var transactionDuration = finishTime - startTime;
				// **************************************************

				// **************************************************
				var transactionAmount =
					(-1) * request.Amount;

				var transaction =
					new Domain.Transaction
					(userId: user.Id, walletId: wallet.Id,
					amount: transactionAmount, userIP: request.User.IP, serverIP: serverIP)
					{
						ServerIP = serverIP,
						UserIP = request.User.IP,

						TransactionDuration = transactionDuration,

						DepositeOrWithdrawProviderName = null,
						DepositeOrWithdrawReferenceCode = null,
						PaymentReferenceCode = request.ReferenceCode,

						AdditionalData = request.AdditionalData,
						UserDescription = request.UserDescription,
						SystemicDescription = request.SystemicDescription,
					};

				transaction.UpdateHash();

				DatabaseContext.Add(entity: transaction);
				// **************************************************

				// **************************************************
				// ذخیره تغییرات در بانک اطلاعاتی
				// **************************************************
				DatabaseContext.SaveChanges();
				// **************************************************

				// **************************************************
				var paymentResponseDto =
					new Dtos.Users.PaymentResponseDto
					(balance: userWallet.Balance, transactionId: transaction.Id);

				result.Data =
					paymentResponseDto;

				return Ok(value: result);
				// **************************************************
			}
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_Payment,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Action: Payment()
}
