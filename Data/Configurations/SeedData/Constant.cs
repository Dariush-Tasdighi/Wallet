namespace Data.Configurations.SeedData;

public static class Constant : object
{
	static Constant()
	{
	}

	public static class Id : object
	{
		static Id()
		{
		}

		public const long User = 101;

		public const long Wallet = 201;

		public const long Company = 301;

		public const long ValidIP1 = 401;

		public const long ValidIP2 = 402;

		public const long UserWallet = 501;

		public const long CompanyWallet = 601;
	}

	public static class Token : object
	{
		static Token()
		{
		}

		public static System.Guid Wallet =
			new(g: "D630496E-3F91-4127-9DBC-F03B14ECD6D2");

		public static System.Guid Company =
			new(g: "D24295E9-DAC0-4FE3-957F-6674F9FD0728");
	}
}
