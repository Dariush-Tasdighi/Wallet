using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers.Admin;

[Microsoft.AspNetCore.Mvc.Route(template: Infrastructure.Constant.DefaultAdminRoute)]
public class TransactionsController : Infrastructure.ControllerBaseWithDatabaseContext
{
	#region Constructor
	public TransactionsController
		(ILogger<TransactionsController> logger,
		Data.DatabaseContext databaseContext, Infrastructure.IUtility utility) :
		base(databaseContext: databaseContext)
	{
		Logger = logger;
		Utility = utility;
	}
	#endregion /Constructor

	#region Properties
	private Infrastructure.IUtility Utility { get; }

	private ILogger<TransactionsController> Logger { get; }
	#endregion /Properties

	#region Action: GetTransactions()
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "[action]")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Dtat.Result<Dtos.Admins.Transactions.GetTransactionsResponseDto>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<Dtat.Result<Dtos.Admins.Transactions.GetTransactionsResponseDto>>>
		GetTransactions(Dtos.Admins.Transactions.GetTransactionsRequestDto request)
	{
		try
		{
			var result = new Dtat.Result
				<Dtos.Admins.Transactions.GetTransactionsResponseDto>();

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
			// **************************************************
			// **************************************************
			var query =
				DatabaseContext.Transactions.AsQueryable()
				.AsNoTracking()
				.Where(current => current.WalletId == wallet.Id)
				;
			// **************************************************

			// **************************************************
			if (string.IsNullOrWhiteSpace(value: request.User.CellPhoneNumber) == false)
			{
				query =
					query
					.Where(current => current.User != null && current.User.CellPhoneNumber == request.User.CellPhoneNumber);
			}
			// **************************************************

			// **************************************************
			if (request.Type.HasValue)
			{
				query =
					query
					.Where(current => current.Type == request.Type.Value);
			}
			// **************************************************

			// **************************************************
			if (request.FromDate.HasValue)
			{
				query =
					query
					.Where(current => current.InsertDateTime >= request.FromDate.Value.Date);
			}

			if (request.ToDate.HasValue)
			{
				query =
					query
					.Where(current => current.InsertDateTime <= request.ToDate.Value.Date);
			}
			// **************************************************

			// **************************************************
			if (request.MinimumAmount.HasValue)
			{
				query =
					query
					.Where(current => current.Amount >= request.MinimumAmount.Value);
			}

			if (request.MaximumAmount.HasValue)
			{
				query =
					query
					.Where(current => current.Amount <= request.MaximumAmount.Value);
			}
			// **************************************************

			// **************************************************
			var foundedItems =
				await
				query
				.OrderBy(current => current.InsertDateTime)
				.Skip(count: request.Skip)
				.Take(count: request.PageSize)
				.Include(current => current.User)
				.Select(current => new Dtos.Admins.Transactions.GetTransactionResponseDto
				{
					Type = current.Type,
					UserIP = current.UserIP,
					Amount = current.Amount,
					UserId = current.UserId,
					WalletId = current.WalletId,
					IsCleared = current.IsCleared,
					WithdrawDate = current.WithdrawDate,
					DisplayName = current!.User!.DisplayName,
					AdditionalData = current.AdditionalData,
					InsertDateTime = current.InsertDateTime,
					UserDescription = current.UserDescription,
					CellPhoneNumber = current.CellPhoneNumber,
					SystemicDescription = current.SystemicDescription,
					PaymentReferenceCode = current.PaymentReferenceCode,
					DepositeOrWithdrawProviderName = current.DepositeOrWithdrawProviderName,
					DepositeOrWithdrawReferenceCode = current.DepositeOrWithdrawReferenceCode,
				})
				.ToListAsync()
				;
			// **************************************************

			// **************************************************
			var totalCount =
				await
				query.CountAsync();
			// **************************************************

			// **************************************************
			var depositeTotalAmount =
				await
				query
				.Where(current => current.Type == Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite)
				.SumAsync(current => current.Amount);

			var withdrawTotalAmount =
				await
				query
				.Where(current => current.Type == Dtat.Wallet.Abstractions.SeedWork.TransactionType.Withdraw)
				.SumAsync(current => current.Amount);
			// **************************************************

			// **************************************************
			var depositeCurrentItemsTotalAmount =
				foundedItems
				.Where(current => current.Type == Dtat.Wallet.Abstractions.SeedWork.TransactionType.Deposite)
				.Sum(current => current.Amount);

			var withdrawCurrentItemsTotalAmount =
				foundedItems
				.Where(current => current.Type == Dtat.Wallet.Abstractions.SeedWork.TransactionType.Withdraw)
				.Sum(current => current.Amount);
			// **************************************************
			// **************************************************
			// **************************************************

			result.Data =
				new Dtos.Admins.Transactions.GetTransactionsResponseDto
				{
					Items = foundedItems,
					TotalCount = totalCount,
					DepositeTotalAmount = depositeTotalAmount,
					WithdrawTotalAmount = withdrawTotalAmount,
					DepositeCurrentItemsTotalAmount = depositeCurrentItemsTotalAmount,
					WithdrawCurrentItemsTotalAmount = withdrawCurrentItemsTotalAmount,
				};

			return Ok(value: result);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Admin_TransactionsController_GetAllTransactionsAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /Action: GetTransactions()

	#region GetTransactionByIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Transaction),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Transaction>> GetTransactionByIdAsync(long id)
	{
		try
		{
			var item =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.Id == id)
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
				(code: Infrastructure.Constant.ErrorCode.Admin_TransactionsController_GetTransactionByIdAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetTransactionByIdAsync()

	#region GetTransactionByHashAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{hash}/hash")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Transaction),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Transaction>>
		GetTransactionByHashAsync(string? hash)
	{
		try
		{
			var item =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.Hash == hash)
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
				(code: Infrastructure.Constant.ErrorCode.Admin_TransactionsController_GetTransactionByHashAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetTransactionByHashAsync()

	#region GetTransactionsByUserIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}/user")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Domain.Transaction>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<System.Collections.Generic.IEnumerable<Domain.Transaction>>> GetTransactionsByUserIdAsync(long id)
	{
		try
		{
			var items =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.UserId == id)
				.ToListAsync();

			return Ok(value: items);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Admin_TransactionsController_GetTransactionsByUserIdAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetTransactionsByUserIdAsync()

	#region GetTransactionsByWalletIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}/wallet")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Domain.Transaction>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<System.Collections.Generic.IEnumerable<Domain.Transaction>>> GetTransactionsByWalletIdAsync(long id)
	{
		try
		{
			var items =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.WalletId == id)
				.ToListAsync();

			return Ok(value: items);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Admin_TransactionsController_GetTransactionsByWalletIdAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetTransactionsByWalletIdAsync()

	#region GetTransactionByPaymentReferenceCodeAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{paymentReferenceCode}/payment-reference-code")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Transaction),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Transaction>>
		GetTransactionByPaymentReferenceCodeAsync(string? paymentReferenceCode)
	{
		try
		{
			var item =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.PaymentReferenceCode == paymentReferenceCode)
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
				(code: Infrastructure.Constant.ErrorCode.Admin_TransactionsController_GetTransactionByPaymentReferenceCodeAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetTransactionByPaymentReferenceCodeAsync()

	#region GetTransactionByDepositeOrWithdrawReferenceCodeAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet
		(template: "{referenceCode}/deposite-or-withdraw")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Transaction),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Transaction>>
		GetTransactionByDepositeOrWithdrawReferenceCodeAsync(string referenceCode)
	{
		try
		{
			var item =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.Where(current => current.DepositeOrWithdrawReferenceCode == referenceCode)
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
				(code: Infrastructure.Constant.ErrorCode.Admin_TransactionsController_GetTransactionByDepositeOrWithdrawReferenceCodeAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetTransactionByDepositeOrWithdrawReferenceCodeAsync()
}
