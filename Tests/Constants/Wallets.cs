namespace Tests.Constants;

internal static class Wallets : object
{
	static Wallets()
	{
	}

	// **************************************************
	public static System.Guid HastiWalletToken =
		new(g: "55308747-A312-4171-9025-901B3AF4F435");

	public static Domain.Wallet HastiWallet = new(name: "کیف پول هستی")
	{
		IsActive = true,
	};
	// **************************************************

	// **************************************************
	public static System.Guid MyTestWalletToken =
		new(g: "8B282AC3-7451-4F95-B5E6-544B738FE19A");

	public static Domain.Wallet MyTestWallet = new(name: "کیف پول تستی من")
	{
		IsActive = true,
	};
	// **************************************************
}
