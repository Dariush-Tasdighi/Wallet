namespace Dtat.Wallet.Abstractions;

public interface ICompanyWallet<T> :
	SeedWork.IEntity<T>, SeedWork.IHasIsActive<T> where T : struct
{
	T WalletId { get; }

	T CompanyId { get; }



	bool IsOwner { get; }



	string? Description { get; }

	string? AdditionalData { get; }



	System.DateTime UpdateDateTime { get; }
}
