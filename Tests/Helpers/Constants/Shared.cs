namespace Tests.Helpers.Constants;

internal static class Shared : object
{
	static Shared()
	{
	}

	internal const decimal Amount = 100_000_000;

	internal const int WithdrawDurationInDays = 0;

	internal const string IranKishProviderName = "Iran Kish";

	internal const string DatabaseCollection = "Database-Collection";

	internal static class Actor : object
	{
		static Actor()
		{
		}

		internal const string IP = "127.0.0.1";

		internal const string Reza = "رضا قدیمی";

		internal const string NationalCode = "1234554321";

		internal const string CellPhoneNumber = "09215149218";

		internal const string EmailAddress = "RezaQadimi.ir@Gmail.com";
	}

	internal static class Wallet : object
	{
		static Wallet()
		{
		}

		internal const string Hit = "کیف پول هستی";

		internal static System.Guid Token =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");
	}

	internal static class Company : object
	{
		static Company()
		{
		}

		internal const string Hit = "شرکت داد و ستد هستی";

		internal const string ServerIP = "192.168.1.110";

		internal static System.Guid Token =
			new System.Guid(g: "FDA05523-DF76-4714-BE2A-A8A385E0CAB2");
	}
}
