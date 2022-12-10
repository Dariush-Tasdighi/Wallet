namespace Dtat.Wallet.Abstractions;

public interface ICompanyWallet<T> :
	SeedWork.IEntity<T>, SeedWork.IHasUpdateDateTime,
	SeedWork.IHasIsActive where T : struct
{
	T WalletId { get; }

	T CompanyId { get; }



	bool IsOwner { get; }



	string? Description { get; }

	string? AdditionalData { get; }
}
