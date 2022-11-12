namespace Infrastructure;

[Microsoft.AspNetCore.Mvc.ApiController]
[Microsoft.AspNetCore.Mvc.Route(template: Constant.DefaultRoute)]

public class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
{
	public ControllerBase() : base()
	{
	}
}
