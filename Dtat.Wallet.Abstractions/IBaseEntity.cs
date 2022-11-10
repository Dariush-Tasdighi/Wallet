namespace Dtat.Wallet.Abstractions
{
	public interface IBaseEntity<T>
	{
		T Id { get; }

		System.DateTime InsertDateTime { get; }
	}
}
