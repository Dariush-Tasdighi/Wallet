using System.Linq;

namespace Infrastructure;

public static class Utility : object
{
	static Utility()
	{
	}

	public static string? GetServerIP(Microsoft.AspNetCore.Http.HttpRequest request)
	{
		System.Net.IPAddress? ip = null;

		var headers =
			request.Headers
			.ToList()
			;

		if (headers.Exists(current => current.Key == "X-Forwarded-For"))
		{
			// When running behind a load balancer you can expect this header
			var header =
				headers
				.Where(current => current.Key == "X-Forwarded-For")
				.First()
				.Value
				.ToString();

			// In case the IP contains a port, remove ':' and everything after
			ip = System.Net.IPAddress
				.Parse(header.Remove(header.IndexOf(':')));
		}
		else
		{
			// This will always have a value (running locally in development won't have the header)
			ip = request.HttpContext.Connection.RemoteIpAddress;
		}

		if (ip == null)
		{
			return null;
		}

		var result =
			ip.ToString();

		return result;
	}
}
