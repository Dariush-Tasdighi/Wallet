namespace Dtat.Wallet.Abstractions.SeedWork;

public interface IHasIsActive<T>
{
	bool IsActive { get; }
}
