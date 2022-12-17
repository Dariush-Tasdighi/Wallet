using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers.Admin;

[Microsoft.AspNetCore.Mvc.Route
	(template: Infrastructure.Constant.DefaultAdminRoute)]
public class CompaniesController :
	Infrastructure.ControllerBaseWithDatabaseContext
{
	#region Constructor
	public CompaniesController
		(ILogger<CompaniesController> logger,
		Data.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}
	#endregion /Constructor

	#region Properties
	private ILogger<CompaniesController> Logger { get; }
	#endregion /Properties

	#region GetAllCompaniesAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Domain.Company>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> GetAllCompaniesAsync()
	{
		try
		{
			//var items =
			//	DatabaseContext.Companies
			//	.ToList()
			//	;

			//var items =
			//	await
			//	DatabaseContext.Companies
			//	.ToListAsync()
			//	;

			var items =
				await
				DatabaseContext.Companies
				.AsNoTracking()
				.ToListAsync()
				;

			return Ok(value: items);
		}
		catch (System.Exception ex)
		{
			//return StatusCode(statusCode: Microsoft.AspNetCore
			//	.Http.StatusCodes.Status500InternalServerError, value: ex.Message);

			// Log: ex or ex.Message for Developer!
			// Send Message: Unexpected Error for end user!

			// Log: ex or ex.Message for Developer!
			// Send Code and Message: Unexpected Error for end user!

			var applicationError =
				new Infrastructure.ApplicationError(code: Infrastructure
				.Constant.ErrorCode.Admin_CompaniesController_GetAllCompaniesAsync,
				message: ex.Message, innerException: ex);

			// Note: Compile -> Warning!
			//Logger.LogError
			//	(exception: ex, message: applicationError.Message);

			Logger.LogError
				(message: Infrastructure.Constant
				.Message.LogError, args: applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError,
				value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetAllCompaniesAsync()

	#region GetCompanyByIdAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Domain.Company),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Company>> GetCompanyByIdAsync(long id)
	{
		try
		{
			var item =
				await
				DatabaseContext.Companies
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
				(code: Infrastructure.Constant.ErrorCode.Admin_CompaniesController_GetCompanyByIdAsync,
				message: ex.Message, innerException: ex);

			Logger.LogError
				(message: Infrastructure.Constant.Message.LogError, applicationError.Message);

			return StatusCode(statusCode: Microsoft.AspNetCore
				.Http.StatusCodes.Status500InternalServerError, value: applicationError.DisplayMessage);
		}
	}
	#endregion /GetCompanyByIdAsync()
}
