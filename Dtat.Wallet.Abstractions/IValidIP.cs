namespace Dtat.Wallet.Abstractions;

public interface IValidIP<T>
{
	T WalletId { get; }



	bool IsActive { get; }

	string ServerIP { get; }

	string? Description { get; }



	int TotalRequestCount { get; }

	int CurrentDayRequestCount { get; }

	int PreviousDay1RequestCount { get; }

	int PreviousDay2RequestCount { get; }

	int PreviousDay3RequestCount { get; }

	int PreviousDay4RequestCount { get; }

	int PreviousDay5RequestCount { get; }

	int PreviousDay6RequestCount { get; }



	System.DateTime UpdateDateTime { get; }

	System.DateTime? LastRequestDateTime { get; }
}
