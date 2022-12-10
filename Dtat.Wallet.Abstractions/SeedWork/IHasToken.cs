namespace Dtat.Wallet.Abstractions.SeedWork;

public interface IHasToken
{
	System.Guid Token { get; }

	void UpdateToken(System.Guid? token = null);
}
