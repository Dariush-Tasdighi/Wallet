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
		Data.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}
	#endregion /Constructor

	#region Properties
	private ILogger<TransactionsController> Logger { get; }
	#endregion /Properties

	#region GetAllTransactionsAsync()
	[Microsoft.AspNetCore.Mvc.HttpPost]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Domain.Transaction>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<System.Collections.Generic.IEnumerable<Domain.Transaction>>>
		GetAllTransactionsAsync(Dtat.Pagination pagination)
	{
		try
		{
			var items =
				await
				DatabaseContext.Transactions
				.AsNoTracking()
				.OrderBy(current => current.InsertDateTime)
				.Skip(count: pagination.Skip)
				.Take(count: pagination.PageSize)
				.ToListAsync()
				;

			return Ok(value: items);
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
	#endregion /GetAllTransactionsAsync()

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
