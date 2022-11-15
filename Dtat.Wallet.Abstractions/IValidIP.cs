namespace Dtat.Wallet.Abstractions
{
	public interface IValidIP<T>
	{
		T WalletId { get; }

		int RequestCount { get; }

		string ServerIP { get; }

		string? Description { get; }

		System.DateTime UpdateDateTime { get; }

		System.DateTime? LastRequestDateTime { get; }
	}
}
