namespace Tests.Constants;

internal static class Wallets : object
{
	static Wallets()
	{
	}

	// **************************************************
	public static Domain.Wallet HastiWallet = new(name: "کیف پول هستی")
	{
		Id = 1,
		IsActive = true,
	};
	// **************************************************

	// **************************************************
	public static Domain.Wallet MyTestWallet = new(name: "کیف پول تستی من")
	{
		IsActive = true,
	};
	// **************************************************
}
