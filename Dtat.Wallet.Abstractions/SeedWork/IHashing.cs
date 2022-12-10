namespace Dtat.Wallet.Abstractions.SeedWork;

public interface IHashing
{
	string? Hash { get; }

	string GetHash();

	void UpdateHash();

	bool CheckHashValidation();
}
