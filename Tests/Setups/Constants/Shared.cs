namespace Tests.Setups.Constants;

internal static class Shared : object
{
	static Shared()
	{
	}

	public const decimal Amount = 100_000_000;

	public const int WithdrawDurationInDays = 0;

	public const string IranKishProviderName = "Iran Kish";

	public const string DatabaseCollection = "Database-Collection";

	internal static class Actor : object
	{
		static Actor()
		{
		}

		public const string IP = "127.0.0.1";

		public const string Reza = "رضا قدیمی";

		public const string NationalCode = "1234554321";

		public const string CellPhoneNumber = "09215149218";

		public const string EmailAddress = "RezaQadimi.ir@Gmail.com";
	}

	internal static class Wallet : object
	{
		static Wallet()
		{
		}

		public const string Hit = "کیف پول هستی";

		public static System.Guid Token =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");
	}

	internal static class Company : object
	{
		static Company()
		{
		}

		public const string Hit = "شرکت داد و ستد هستی";

		public const string ServerIP = "192.168.1.110";

		public static System.Guid Token =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");
	}
}
