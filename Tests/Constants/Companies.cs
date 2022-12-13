namespace Tests.Constants;

internal static class Companies : object
{
	static Companies()
	{
	}

	// **************************************************
	public const string HitIP = "192.168.1.110";

	public static System.Guid HitCompanyToken =
		new(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");

	public static Domain.Company Hit =
		new(name: "شرکت داد و ستد هستی")
		{
			IsActive= true,
		};
	// **************************************************
}
