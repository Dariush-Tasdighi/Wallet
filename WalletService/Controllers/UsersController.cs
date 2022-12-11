using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

public class UsersController :
	Infrastructure.ControllerBaseWithDatabaseContext
{
	private static object Locker = new();

	#region Constructor
	public UsersController
		(ILogger<UsersController> logger,
		Data.DatabaseContext databaseContext, Infrastructure.IUtility utility) :
		base(databaseContext: databaseContext)
	{
		Logger = logger;
		Utility = utility;
	}
	#endregion /Constructor

	#region Properties
	private Infrastructure.IUtility Utility { get; }

	private ILogger<UsersController> Logger { get; }
	#endregion /Properties

	#region Action: GetBalance()
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtat.Result<Dtos.Users.GetBalanceResponseDto>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public Microsoft.AspNetCore.Mvc.ActionResult
		<Dtat.Result<Dtos.Users.GetBalanceResponseDto>>
		GetBalance(Dtos.Users.GetBalanceRequestDto request)
	{
		try
		{
			var result =
				new Dtat.Result<Dtos.Users.GetBalanceResponseDto>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Utility.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(serverIP));

				result.AddErrorMessages
					(message: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			if (request == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(request));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(company));

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
					cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

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
					return Ok(value: walletResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(wallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(companyWallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(user));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی کاربر به کیف پول مربوطه
				// **************************************************
				var userWalletResult =
					Services.UserWalletsService
					.CheckAndGetUserWallet(databaseContext: DatabaseContext,
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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(userWallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بدست آوردن مانده کیف پول کاربر
				// با احتساب چک کردن معتبر بودن آن
				// **************************************************
				var userBalanceResult =
					Services.UserWalletsService.GetUserBalanceWithCheckingDataConsistency
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, userWallet: userWallet);

				if (userBalanceResult.IsSuccess == false)
				{
					return Ok(value: userBalanceResult);
				}
				// **************************************************

				// **************************************************
				// بدست آوردن مانده قابل برداشت
				// **************************************************
				var userWithdrawBalanceResult =
					Services.UserWalletsService.GetUserWithdrawBalance
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

				if (userWithdrawBalanceResult.IsSuccess == false)
				{
					return Ok(value: userWithdrawBalanceResult);
				}
				// **************************************************

				// **************************************************
				var data =
					new Dtos.Users.GetBalanceResponseDto
					(balance: userBalanceResult.Data,
					withdrawBalance: userWithdrawBalanceResult.Data);

				result.Data = data;

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
				Utility.GetNow();

			var result = new Dtat.Result
				<Dtos.Users.DepositeResponseDto>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Utility.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(serverIP));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.Company));

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
					cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

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
					return Ok(value: walletResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.Wallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				if (wallet.DepositeFeatureIsEnabled == false)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.FeatureIsNotEnabled,
						arg0: nameof(Deposite), arg1: nameof(wallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.CompanyWallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNotActive,
						arg0: nameof(Domain.User));

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
					additionalData: request.AdditionalData);

				if (userWallet.IsActive == false)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.ItemIsNotActiveIn,
						arg0: nameof(Domain.User), arg1: nameof(Domain.Wallet));

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
				// ذخیره تغییرات در بانک اطلاعاتی
				// **************************************************
				DatabaseContext.SaveChanges();
				// **************************************************

				// **************************************************
				// محاسبه کل زمان پردازش
				// **************************************************
				var finishTime =
					Utility.GetNow();

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

						Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite,
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
				// TODO
				var depositeResponseDto =
					new Dtos.Users.DepositeResponseDto
					(balance: userWallet.Balance, withdrawBalance: 0, transactionId: transaction.Id);

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
				Utility.GetNow();

			var result = new Dtat.Result
				<Dtos.Users.PaymentResponseDto>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Utility.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(serverIP));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.Company));

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
					cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

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
					return Ok(value: walletResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.Wallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				if (wallet.PaymentFeatureIsEnabled == false)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.FeatureIsNotEnabled,
						arg0: nameof(Payment), arg1: nameof(Domain.Wallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.CompanyWallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.User));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(Domain.UserWallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheAmountValueIsMore,
						arg0: nameof(Domain.UserWallet.Balance));

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
					Utility.GetNow();

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

						Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Payment,
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
				// TODO
				var paymentResponseDto =
					new Dtos.Users.PaymentResponseDto
					(balance: userWallet.Balance, withdrawBalance: 0, transactionId: transaction.Id);

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

	#region Action: Withdraw()
	/// <summary>
	/// تعریف نمی‌کنیم Async به دلیل مسائل امنیتی و هم‌زمانی این تابع را
	/// </summary>
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtat.Result<Dtos.Users.WithdrawResponseDto>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public Microsoft.AspNetCore.Mvc.ActionResult Withdraw(Dtos.Users.WithdrawRequestDto request)
	{
		try
		{
			var startTime =
				Utility.GetNow();

			var result = new Dtat.Result
				<Dtos.Users.WithdrawResponseDto>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Utility.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(serverIP));

				result.AddErrorMessages
					(message: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			if (request == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(request));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(company));

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
					cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

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
					return Ok(value: walletResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(wallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				if (wallet.WithdrawFeatureIsEnabled == false)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.FeatureIsNotEnabled,
						arg0: nameof(Withdraw), arg1: nameof(Domain.Wallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(companyWallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(user));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(userWallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بدست آوردن مانده کیف پول کاربر
				// با احتساب چک کردن معتبر بودن آن
				// **************************************************
				var userBalanceResult =
					Services.UserWalletsService.GetUserBalanceWithCheckingDataConsistency
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, userWallet: userWallet);

				if (userBalanceResult.IsSuccess == false)
				{
					return Ok(value: userBalanceResult);
				}

				var userBalance =
					userBalanceResult.Data;
				// **************************************************

				// **************************************************
				// بدست آوردن مانده قابل برداشت
				// **************************************************
				var userWithdrawBalanceResult =
					Services.UserWalletsService.GetUserWithdrawBalance
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

				if (userWithdrawBalanceResult.IsSuccess == false)
				{
					return Ok(value: userWithdrawBalanceResult);
				}

				var userWithdrawBalance =
					userWithdrawBalanceResult.Data;
				// **************************************************

				// **************************************************
				// کاهش مانده حساب کاربر در کیف پول جاری
				// **************************************************
				if (request.Amount > userWithdrawBalance)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheAmountValueIsMore,
						arg0: $"{nameof(Withdraw)} {nameof(Domain.UserWallet.Balance)}");

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				userWallet.Balance -= request.Amount;

				userWallet.UpdateHash();
				// **************************************************

				// **************************************************
				// محاسبه کل زمان پردازش
				// **************************************************
				var finishTime =
					Utility.GetNow();

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

						PaymentReferenceCode = null,
						DepositeOrWithdrawProviderName = null,
						DepositeOrWithdrawReferenceCode = null,

						AdditionalData = request.AdditionalData,
						UserDescription = request.UserDescription,
						SystemicDescription = request.SystemicDescription,

						Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Withdraw,
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
				var data =
					new Dtos.Users.WithdrawResponseDto(balance: userBalance,
					withdrawBalance: userWithdrawBalance, transactionId: transaction.Id);

				result.Data = data;

				return Ok(value: result);
				// **************************************************
			}
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_Withdraw,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Action: Withdraw()

	#region Action: Refund()
	/// <summary>
	/// تعریف نمی‌کنیم Async به دلیل مسائل امنیتی و هم‌زمانی این تابع را
	/// </summary>
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtat.Result<Dtos.Users.RefundResponseDto>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public Microsoft.AspNetCore.Mvc.ActionResult Refund(Dtos.Users.RefundRequestDto request)
	{
		try
		{
			var startTime =
				Utility.GetNow();

			var result = new Dtat.Result
				<Dtos.Users.RefundResponseDto>();

			// **************************************************
			// بدست آوردن آی‌پی سرور درخواست کننده
			// **************************************************
			var serverIP =
				Utility.GetServerIP(request: Request);

			if (serverIP == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(serverIP));

				result.AddErrorMessages
					(message: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			if (request == null)
			{
				var errorMessage = string.Format
					(format: Resources.Messages.Errors.TheItemIsNull,
					arg0: nameof(request));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(company));

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
					cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

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
					return Ok(value: walletResult);
				}

				var wallet =
					walletResult.Data;

				// بودن null صرفا برای جلوگیری از اخطار
				if (wallet == null)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(wallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				// فعال است Refund امکان
				if (wallet.RefundFeatureIsEnabled == false)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.FeatureIsNotEnabled,
						arg0: nameof(Refund), arg1: nameof(Domain.Wallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی دسترسی شرکت به کیف پول مربوطه، بر اساس توکن‌های آن‌ها
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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(companyWallet));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(user));

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
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheItemIsNull,
						arg0: nameof(userWallet));

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بدست آوردن مانده کیف پول کاربر
				// با احتساب چک کردن معتبر بودن آن
				// **************************************************
				var userBalanceResult =
					Services.UserWalletsService.GetUserBalanceWithCheckingDataConsistency
					(databaseContext: DatabaseContext, walletToken: request.WalletToken,
					cellPhoneNumber: request.User.CellPhoneNumber, userWallet: userWallet);

				if (userBalanceResult.IsSuccess == false)
				{
					return Ok(value: userBalanceResult);
				}

				var userBalance =
					userBalanceResult.Data;
				// **************************************************

				// **************************************************
				// Refund بدست آوردن مبلغ قابل
				// **************************************************
				var userRefundableBalanceResult =
					Services.UserWalletsService.GetUserRefundableBalance
					(databaseContext: DatabaseContext, transactionId: request.TransactionId,
					walletToken: request.WalletToken, cellPhoneNumber: request.User.CellPhoneNumber, utility: Utility);

				if (userRefundableBalanceResult.IsSuccess == false)
				{
					return Ok(value: userRefundableBalanceResult);
				}

				var userRefundBalance =
					userRefundableBalanceResult.Data;
				// **************************************************

				// **************************************************
				if (request.Amount > userRefundBalance)
				{
					var errorMessage = string.Format
						(format: Resources.Messages.Errors.TheAmountValueIsMore,
						arg0: $"{nameof(Refund)} {nameof(Domain.UserWallet.Balance)}");

					result.AddErrorMessages
						(message: errorMessage);

					return Ok(value: result);
				}

				userWallet.Balance += request.Amount;

				userWallet.UpdateHash();
				// **************************************************

				// **************************************************
				// ذخیره تغییرات در بانک اطلاعاتی
				// **************************************************
				DatabaseContext.SaveChanges();
				// **************************************************

				// **************************************************
				// محاسبه کل زمان پردازش
				// **************************************************
				var finishTime =
					Utility.GetNow();

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

						PaymentReferenceCode = null,
						DepositeOrWithdrawProviderName = null,
						DepositeOrWithdrawReferenceCode = null,
						ParentTransactionId = request.TransactionId,

						AdditionalData = request.AdditionalData,
						UserDescription = request.UserDescription,
						SystemicDescription = request.SystemicDescription,

						Type = Dtat.Wallet.Abstractions.SeedWork.TransactionType.Refund,
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
				var data =
					new Dtos.Users.RefundResponseDto
					(balance: userWallet.Balance, withdrawBalance: 0, transactionId: transaction.Id);

				result.Data = data;

				return Ok(value: result);
				// **************************************************
			}
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_Refund,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Action: Refund()

	#region Action: GetTransactionByIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}/transaction")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtos.Users.GetTransactionResponseDto),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Dtos.Users.GetTransactionResponseDto>>
		GetTransactionByPaymentReferenceCodeAsync(long id)
	{
		try
		{
			var item =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.Id == id)
				.Select(current => new Dtos.Users.GetTransactionResponseDto
				{
					Type = current.Type,
					UserIP = current.UserIP,
					Amount = current.Amount,
					UserId = current.UserId,
					WalletId = current.WalletId,
					IsCleared = current.IsCleared,
					PartyUserId = current.PartyUserId,
					WithdrawDate = current.WithdrawDate,
					AdditionalData = current.AdditionalData,
					InsertDateTime = current.InsertDateTime,
					UserDescription = current.UserDescription,
					SystemicDescription = current.SystemicDescription,
					PaymentReferenceCode = current.PaymentReferenceCode,
					DepositeOrWithdrawProviderName = current.DepositeOrWithdrawProviderName,
					DepositeOrWithdrawReferenceCode = current.DepositeOrWithdrawReferenceCode,
				})
				.FirstOrDefaultAsync();

			if (item == null)
			{
				return NotFound(value: Infrastructure.Constant.Message.NotFound);
			}

			return Ok(value: item);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_GetTransactionById,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Action: GetTransactionByIdAsync()

	#region Action: GetUserTransactionsByCellPhoneNumberAsync()
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "transactions/cellPhoneNumber")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Dtos.Users.GetTransactionResponseDto>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<System.Collections.Generic.IEnumerable<Dtos.Users.GetTransactionResponseDto>>>
		GetUserTransactionsByCellPhoneNumberAsync
		(Dtos.Users.GetTransactionRequestDto request)
	{
		try
		{
			var items =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.User != null && current.User.CellPhoneNumber == request.CellPhoneNumber)
				.OrderBy(current => current.InsertDateTime)
				.Skip(count: request.Skip)
				.Take(count: request.PageSize)
				.Select(current => new Dtos.Users.GetTransactionResponseDto
				{
					Type = current.Type,
					UserIP = current.UserIP,
					Amount = current.Amount,
					UserId = current.UserId,
					WalletId = current.WalletId,
					IsCleared = current.IsCleared,
					PartyUserId = current.PartyUserId,
					WithdrawDate = current.WithdrawDate,
					AdditionalData = current.AdditionalData,
					InsertDateTime = current.InsertDateTime,
					UserDescription = current.UserDescription,
					SystemicDescription = current.SystemicDescription,
					PaymentReferenceCode = current.PaymentReferenceCode,
					DepositeOrWithdrawProviderName = current.DepositeOrWithdrawProviderName,
					DepositeOrWithdrawReferenceCode = current.DepositeOrWithdrawReferenceCode,
				})
				.ToListAsync()
				;

			return Ok(value: items);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Root_UsersController_GetUserTransactionsByCellPhoneNumber,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Action: GetUserTransactionsByCellPhoneNumberAsync()
}
