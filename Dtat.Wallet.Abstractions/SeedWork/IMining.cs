namespace Dtat.Wallet.Abstractions.SeedWork;

public interface IMining<T> : IHashing<T>
{
	int Nonce { get; }

	int Difficulty { get; }

	int BlockNumber { get; }

	string? ParentHash { get; }

	System.TimeSpan? MiningDuration { get; }
}
