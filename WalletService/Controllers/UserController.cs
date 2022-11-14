using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter.Xml;
using System.Collections.Generic;

namespace Server.Controllers;

public class UsersController : Infrastructure.ControllerBaseWithDatabaseContext
{
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

	//#region GetUserByIdAsync()
	//[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(Domain.User),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	//public async System.Threading.Tasks.Task
	//	<Microsoft.AspNetCore.Mvc.ActionResult<Domain.User>> GetUserByIdAsync(long id)
	//{
	//	try
	//	{
	//		var item =
	//			await
	//			DatabaseContext.Users
	//			.AsNoTracking()
	//			.Where(current => current.Id == id)
	//			.FirstOrDefaultAsync();

	//		if (item == null)
	//		{
	//			return NotFound(value: Infrastructure.Constant.Message.NotFound);
	//		}

	//		return Ok(value: item);
	//	}
	//	catch (System.Exception ex)
	//	{
	//		var applicationError =
	//			new Infrastructure.ApplicationError
	//			(code: Infrastructure.Constant.ErrorCode.Root_UsersController_GetUserByIdAsync,
	//			message: ex.Message, innerException: ex);

	//		Logger.LogError
	//			(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

	//		return StatusCode(statusCode: Microsoft.AspNetCore
	//			.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
	//	}
	//}
	//#endregion /GetUserByIdAsync()

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
		GetBalanceAsync(System.Guid waletToken, string companyUserIdentity)
	{
		try
		{
			var item =
				await
				DatabaseContext.UserWallets
				.AsNoTracking()
				.Where(current => current.IsActive)
				.Where(current => current.User != null && current.User.IsActive)
				.Where(current => current.Wallet != null && current.Wallet.IsActive)
				.Where(current => current.CompanyUserIdentity == companyUserIdentity)
				.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)
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
		GetLastTransactionsAsync(System.Guid waletToken, string companyUserIdentity, int count)
	{
		try
		{
			var foundedUserWallet =
				await
				DatabaseContext.UserWallets
				.AsNoTracking()
				.Where(current => current.IsActive)
				.Where(current => current.User != null && current.User.IsActive)
				.Where(current => current.Wallet != null && current.Wallet.IsActive)
				.Where(current => current.CompanyUserIdentity == companyUserIdentity)
				.Where(current => current.Wallet != null && current.Wallet.Token == waletToken)
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
}
