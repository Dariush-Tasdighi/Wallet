namespace Tests.Constants;

internal static class Companies : object
{
	static Companies()
	{
	}

	// **************************************************
	public static Domain.Company Hit =
		new(name: "شرکت داد و ستد هستی")
		{
			IsActive= true,
		};
	// **************************************************
}
