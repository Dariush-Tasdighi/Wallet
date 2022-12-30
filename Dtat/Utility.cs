namespace Dtat;

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

	public static Result ValidateEntity(object entity)
	{
		var result =
			new Result();

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
			result.AddErrorMessages(message: item.ErrorMessage);
		}

		return result;
	}

	public static string GetSha256(string text)
	{
		var inputBytes =
			System.Text.Encoding.UTF8.GetBytes(s: text);

		var sha =
			System.Security.Cryptography.SHA256.Create();

		var outputBytes =
			sha.ComputeHash(buffer: inputBytes);

		sha.Dispose();
		//sha = null;

		var result =
			System.Convert.ToBase64String(inArray: outputBytes);

		return result;
	}
}
