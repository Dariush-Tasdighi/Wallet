namespace Tests.Helpers;

internal static class Utility : object
{
	static Utility()
	{
	}

	internal static string ReferenceCode
	{
		get
		{
			var referenceCode = new System.Random()
				.NextInt64(minValue: 1000000000, maxValue: 9999999999)
				.ToString();

			return referenceCode;
		}
	}
}
