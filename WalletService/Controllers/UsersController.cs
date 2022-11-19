﻿using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

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

	#region Action: GetBalanceAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet
		(template: "[action]/{waletToken}/{cellPhoneNumber}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Wallet),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(long),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
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

			return Ok(value: item.GetBalance());
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
	#endregion /Action: GetBalanceAsync()

	#region Action: GetLastTransactionsAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet
		(template: "[action]/{waletToken}/{cellPhoneNumber}/{count}")]

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

			//	.Where(current => current.Wallet != null && current.Wallet.IsActive)
			//	.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)

			//	.Where(current => current.User != null && current.User.IsActive)
			//	.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
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
	#endregion /Action: GetLastTransactions()

	#region Action: Deposite()
	/// <summary>
	/// تعریف نمی‌کنیم Async به دلیل مسائل امنیتی و هم‌زمانی این تابع را
	/// </summary>
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Infrastructure.Result<Dtos.Users.DepositeResponseDto>),
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
			var startTime =
				System.DateTime.Now;

			var result =
				new Infrastructure
				.Result<Dtos.Users.DepositeResponseDto>();

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
					(errorMessage: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			// بررسی مجاز بودن آی‌پی سرور درخواست کننده
			// **************************************************
			var isServerIPValid = ValidateServerIP
				(waletToken: request.WaletToken, serverIP: serverIP);

			if (isServerIPValid == false)
			{
				var errorMessage =
					$"Server IP is not valid for this wallet!";

				result.AddErrorMessages
					(errorMessage: errorMessage);

				return Ok(value: result);
			}
			// **************************************************

			// **************************************************
			// بررسی معتبر بودن بقیه فیلدهای ارسال شده
			// **************************************************
			var errorMessages =
				Infrastructure.Utility
				.ValidateEntity(entity: request);

			if (errorMessages.Count > 0)
			{
				foreach (var errorMessage in errorMessages)
				{
					result.AddErrorMessages(errorMessage: errorMessage);
				}

				return Ok(value: result);
			}
			// **************************************************

			lock (Locker)
			{
				// **************************************************
				// بررسی کیف پول
				// **************************************************
				var wallet =
					DatabaseContext.Wallets
					.Where(current => current.Token == request.WaletToken)
					.FirstOrDefault();

				if (wallet == null)
				{
					var errorMessage =
						$"Wallet not found!";

					result.AddErrorMessages
						(errorMessage: errorMessage);

					return Ok(value: result);
				}

				if (wallet.IsActive == false)
				{
					var errorMessage =
						$"This wallet is not active!";

					result.AddErrorMessages
						(errorMessage: errorMessage);

					return Ok(value: result);
				}

				if (wallet.DepositeFeatureIsEnabled == false)
				{
					var errorMessage =
						$"Deposite feature is not enabled for this wallet!";

					result.AddErrorMessages
						(errorMessage: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی کاربر
				// **************************************************
				var user =
					CreateOrUpdateUser
					(displayName: request.User!.DisplayName!,
					cellPhoneNumber: request.User!.CellPhoneNumber!,

					emailAddress: request.User.EmailAddress,
					nationalCode: request.User.NationalCode);

				if (user.IsActive == false)
				{
					var errorMessage =
						$"User is not active!";

					result.AddErrorMessages
						(errorMessage: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// بررسی عضویت کاربر در کیف پول
				// **************************************************
				var userWallet =
					CreateOrUpdateUserWallet
					(userId: user.Id, walletId: wallet.Id,
					additionalData: request.AdditionalData,
					paymentFeatureIsEnabled: request.User.PaymentFeatureIsEnabled,
					depositeFeatureIsEnabled: request.User.DepositeFeatureIsEnabled,
					withdrawFeatureIsEnabled: request.User.WithdrawFeatureIsEnabled);

				if (userWallet.IsActive == false)
				{
					var errorMessage =
						$"User is not active in this wallet!";

					result.AddErrorMessages
						(errorMessage: errorMessage);

					return Ok(value: result);
				}

				if (userWallet.DepositeFeatureIsEnabled == false)
				{
					var errorMessage =
						$"Deposite feature is not enabled for this user!";

					result.AddErrorMessages
						(errorMessage: errorMessage);

					return Ok(value: result);
				}
				// **************************************************

				// **************************************************
				// افزایش مانده حساب کاربر
				// **************************************************
				userWallet.Deposit(request.Amount);
				// **************************************************

				// **************************************************
				// افزایش مانده حساب کاربر
				// **************************************************
				var finishTime =
					System.DateTime.Now;

				var timeDurationInMillisecond = finishTime - startTime;
				// **************************************************

				// **************************************************
				var transaction =
					new Domain.Transaction
					(userId: user.Id, walletId: wallet.Id,
					amount: request.Amount, userIP: request.User!.IP!, serverIP: serverIP)
					{
						ServerIP = serverIP,
						UserIP = request.User!.IP!,
						AdditionalData = request.AdditionalData,
						UserDescription = request.UserDescription,
						SystemicDescription = request.SystemicDescription,
						DepositeOrWithdrawProviderName = request.ProviderName,
						DepositeOrWithdrawReferenceCode = request.ReferenceCode,
						TimeDurationInMillisecond = timeDurationInMillisecond.TotalMicroseconds,
					};

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
					(balance: userWallet.GetBalance(), transactionId: transaction.Id);

				result.Data = depositeResponseDto;

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

	#region Method: ValidateServerIP()
	private bool ValidateServerIP(System.Guid waletToken, string serverIP)
	{
		var isValid = false;

		var now =
			Infrastructure.Utility.Now;

		var validIP =
			DatabaseContext.ValidIPs
			.Where(current => current.IsActive)
			.Where(current => current.ServerIP == serverIP)
			.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)
			.FirstOrDefault();

		if (validIP != null)
		{
			isValid = true;

			validIP.TotalRequestCount++;

			if (validIP.LastRequestDateTime.HasValue == false)
			{
				validIP.CurrentDayRequestCount = 1;
			}
			else
			{
				if (now.Date <= validIP.LastRequestDateTime.Value.Date)
				{
					validIP.CurrentDayRequestCount++;
				}
				else
				{
					validIP.PreviousDay6RequestCount = validIP.PreviousDay5RequestCount;
					validIP.PreviousDay5RequestCount = validIP.PreviousDay4RequestCount;
					validIP.PreviousDay4RequestCount = validIP.PreviousDay3RequestCount;
					validIP.PreviousDay3RequestCount = validIP.PreviousDay2RequestCount;
					validIP.PreviousDay2RequestCount = validIP.PreviousDay1RequestCount;
					validIP.PreviousDay1RequestCount = validIP.CurrentDayRequestCount;

					validIP.CurrentDayRequestCount = 1;
				}
			}

			validIP.LastRequestDateTime = now;
		}
		else
		{
			// TODO
		}

		DatabaseContext.SaveChanges();
		// **************************************************

		return isValid;
	}
	#endregion /Method: ValidateServerIP()

	#region Method: CreateOrUpdateUser()
	private Domain.User CreateOrUpdateUser
		(string cellPhoneNumber, string displayName,
		string? emailAddress, string? nationalCode)
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
					IsActive = true,

					DisplayName = displayName,
					NationalCode = nationalCode,
					EmailAddress = emailAddress,
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
		}

		return user;
	}
	#endregion /Method: CreateOrUpdateUser()

	#region Method: CreateOrUpdateUserWallet()
	private Domain.UserWallet CreateOrUpdateUserWallet
		(long userId, long walletId,
		bool paymentFeatureIsEnabled,
		bool depositeFeatureIsEnabled,
		bool withdrawFeatureIsEnabled,
		string? additionalData)
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
					IsActive = true,

					AdditionalData = additionalData,
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
			userWallet.AdditionalData = additionalData;
			userWallet.PaymentFeatureIsEnabled = paymentFeatureIsEnabled;
			userWallet.DepositeFeatureIsEnabled = depositeFeatureIsEnabled;
			userWallet.WithdrawFeatureIsEnabled = withdrawFeatureIsEnabled;
		}

		return userWallet;
	}
	#endregion /Method: CreateOrUpdateUserWallet()
}
