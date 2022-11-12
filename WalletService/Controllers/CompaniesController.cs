using System.Linq;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Server.Controllers;

public class CompaniesController : Infrastructure.ControllerBaseWithDatabaseContext
{
	public CompaniesController
		(Microsoft.Extensions.Logging.ILogger<CompaniesController> logger,
		Data.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
		Logger = logger;
	}

	private Microsoft.Extensions.Logging.ILogger<CompaniesController> Logger { get; }

	[Microsoft.AspNetCore.Mvc.HttpGet]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult
		<System.Collections.Generic.IEnumerable<Domain.Company>>> Get()
	{
		try
		{
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
			Logger.LogError
				(message: Infrastructure.Constant.ErrorMessage, ex.Message);

			return BadRequest(error: ex.Message);
		}
	}

	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]
	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.ActionResult<Domain.Company>> Get(long id)
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
				return NotFound();
			}

			return Ok(value: item);
		}
		catch (System.Exception ex)
		{
			Logger.LogError
				(message: Infrastructure.Constant.ErrorMessage, ex.Message);

			return BadRequest(error: ex.Message);
		}
	}
}
