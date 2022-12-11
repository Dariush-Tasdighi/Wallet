using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers.Admin;

[Microsoft.AspNetCore.Mvc.Route(template: Infrastructure.Constant.DefaultAdminRoute)]
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

	#region GetAllUsersAsync()
	[Microsoft.AspNetCore.Mvc.HttpPost]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Domain.User>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<System.Collections.Generic.IEnumerable<Domain.User>>>
		GetAllUsersAsync(Dtat.Pagination pagination)
	{
		try
		{
			var items =
				await
				DatabaseContext.Users
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
				(code: Infrastructure.Constant.ErrorCode.Admin_UsersController_GetAllUsersAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetAllUsersAsync()

	#region GetUserByIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.User),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.User>> GetUserByIdAsync(long id)
	{
		try
		{
			var item =
				await
				DatabaseContext.Users
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
				(code: Infrastructure.Constant.ErrorCode.Admin_UsersController_GetUserByIdAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetUserByIdAsync()
}
