using System.Net.Mail;

namespace Dtat.Wallet.Abstractions.SeedWork;

public interface IHashing<T>
{
	string? Hash { get; }

	string GetHash();

	void UpdateHash();

	bool CheckHashValidation();
}
