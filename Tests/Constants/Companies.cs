namespace Tests.Constants;

internal static class Companies : object
{
	static Companies()
	{
	}

	// **************************************************
	public const string HitIP = "192.168.1.110";

	public static System.Guid HitCompanyToken =
		new(g: "D24295E9-DAC0-4FE3-957F-6674F9FD0728");

	public static Domain.Company Hit =
		new(name: "شرکت داد و ستد هستی")
		{
			IsActive= true,
		};
	// **************************************************
}
