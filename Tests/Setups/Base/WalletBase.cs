namespace Tests.Setups.Base;

internal abstract class WalletBase : object
{
	protected WalletBase() : base()
	{
		IP = string.Empty;

		Wallet =
			new(name: string.Empty);
	}


	// **************************************************
	public string IP { get; protected set; }

	public System.Guid Token { get; protected set; }

	public Domain.Wallet Wallet { get; protected set; }
	// **************************************************
}
