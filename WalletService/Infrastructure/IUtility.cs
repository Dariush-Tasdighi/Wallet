namespace Infrastructure;

public interface IUtility
{
	System.DateTime GetNow();

	string? GetServerIP(Microsoft.AspNetCore.Http.HttpRequest request);
}
