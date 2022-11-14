namespace Dtat.Wallet.Abstractions
{
	public interface IValidIP<T>
	{
		T WalletId { get; }

		string ServerIP { get; }

		int RequestCount { get; }

		System.DateTime? LastRequestDateTime { get; }
	}
}
