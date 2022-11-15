using System.Linq;

namespace Infrastructure;

public static class Utility : object
{
	static Utility()
	{
	}

	public static System.DateTime Now
	{
		get
		{
			var result =
				System.DateTime.Now;

			return result;
		}
	}

	public static string? FixText(string? text)
	{
		if (text == null)
		{
			return null;
		}

		text =
			text.Trim();

		if (text == string.Empty)
		{
			return null;
		}

		while (text.Contains("  "))
		{
			text =
				text.Replace("  ", " ");
		}

		return text;
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

	public static System.Collections.Generic.IList<string> ValidateEntity(object entity)
	{
		var result =
			new System.Collections.Generic.List<string>();

		var validationContext =
			new System.ComponentModel
			.DataAnnotations.ValidationContext(instance: entity);

		var validationResults =
			new System.Collections.Generic.List
			<System.ComponentModel.DataAnnotations.ValidationResult>();

		System.ComponentModel.DataAnnotations.Validator
			.TryValidateObject(instance: entity, validationContext: validationContext,
			validationResults: validationResults, validateAllProperties: true);

		foreach (var item in validationResults)
		{
			if (string.IsNullOrWhiteSpace(value: item.ErrorMessage) == false)
			{
				result.Add(item: item.ErrorMessage);
			}
		}

		return result;
	}
}
