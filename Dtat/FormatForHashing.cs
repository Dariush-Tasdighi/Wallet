namespace Dtat;

public static class ConvertForHashing : object
{
	public const string Null = "Null";

	static ConvertForHashing()
	{
	}

	public static string FromString(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			return Null;
		}
		else
		{
			var result =
				value.ToString();

			return result;
		}
	}

	public static string FromDecimal(decimal? value)
	{
		if (value.HasValue == false)
		{
			return Null;
		}
		else
		{
			var result =
				value.Value.ToString(format: "0.0");

			return result;
		}
	}

	public static string FromDateTime(System.DateTime? value)
	{
		if (value.HasValue == false)
		{
			return Null;
		}
		else
		{
			var result =
				value.Value.ToString
				(format: "yyyy/MM/dd - HH:mm:ss");

			return result;
		}
	}
}
