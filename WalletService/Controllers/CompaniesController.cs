using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Server.Controllers
{
	public class CompaniesController : Infrastructure.ControllerBaseWithDatabaseContext
	{
		public CompaniesController
			(Data.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
		{
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		public async System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult
			<System.Collections.Generic.IEnumerable<Domain.Company>>> Get()
		{
			try
			{
				var items =
					await DatabaseContext.Companies.ToListAsync();

				return Ok(items);
			}
			catch (System.Exception ex)
			{
				return BadRequest(error: ex.Message);
			}
		}
	}
}
