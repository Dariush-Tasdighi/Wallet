namespace Dtat.Wallet.Abstractions.SeedWork;

public interface IEntity<T>
{
	T Id { get; }

	System.DateTime InsertDateTime { get; }
}
