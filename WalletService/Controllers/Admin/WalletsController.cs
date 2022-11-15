using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Server.Controllers.Admin;

[Microsoft.AspNetCore.Mvc.Route(template: Infrastructure.Constant.DefaultAdminRoute)]
public class WalletsController : Infrastructure.ControllerBaseWithDatabaseContext
{
	#region Constructor
	public WalletsController
		(ILogger<WalletsController> logger,
		Data.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}
	#endregion /Constructor

	#region Properties
	private ILogger<WalletsController> Logger { get; }
	#endregion /Properties

	#region GetAllWalletsAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Domain.Wallet>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<System.Collections.Generic.IEnumerable<Domain.Wallet>>> GetAllWalletsAsync()
	{
		try
		{
			var items =
				await
				DatabaseContext.Wallets
				.AsNoTracking()
				.ToListAsync()
				;

			return Ok(value: items);
		}
		catch (System.Exception ex)
		{
			var applicationError =
				new Infrastructure.ApplicationError
				(code: Infrastructure.Constant.ErrorCode.Admin_WalletsController_GetAllWalletsAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetAllWalletsAsync()

	#region GetWalletByIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

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
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Wallet>> GetWalletByIdAsync(long id)
	{
		try
		{
			var item =
				await
				DatabaseContext.Wallets
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
				(code: Infrastructure.Constant.ErrorCode.Admin_WalletsController_GetWalletByIdAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetWalletByIdAsync()

	#region GetWalletsByCompanyIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet
		(template: "[action]/{companyId}")]

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
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Wallet>> GetWalletsByCompanyIdAsync(long companyId)
	{
		try
		{
			var item =
				await
				DatabaseContext.Wallets
				.AsNoTracking()
				.Where(current => current.CompanyId == companyId)
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
				(code: Infrastructure.Constant.ErrorCode.Admin_WalletsController_GetWalletByIdAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetWalletsByCompanyIdAsync()
}
