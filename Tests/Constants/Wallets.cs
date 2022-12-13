namespace Tests.Constants;

internal static class Wallets : object
{
	static Wallets()
	{
	}

	// **************************************************
	public static System.Guid HastiWalletToken =
		new(g: "D630496E-3F91-4127-9DBC-F03B14ECD6D2");

	public static Domain.Wallet HastiWallet = new(name: "کیف پول هستی")
	{
		Id = 1,
		IsActive = true,
	};
	// **************************************************

	// **************************************************
	public static System.Guid MyTestWalletToken =
		new(g: "62545A53-85EC-4670-8A24-3F4AD7667B6E");

	public static Domain.Wallet MyTestWallet = new(name: "کیف پول تستی من")
	{
		IsActive = true,
	};
	// **************************************************
}
