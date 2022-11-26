namespace Dtat.Wallet.Abstractions;

public interface ICompanyWallet<T> :
	SeedWork.IEntity<T> where T : struct
{
	T WalletId { get; }

	T CompanyId { get; }



	bool IsOwner { get; }

	bool IsActive { get; }



	string? Description { get; }

	string? AdditionalData { get; }



	System.DateTime UpdateDateTime { get; }
}
