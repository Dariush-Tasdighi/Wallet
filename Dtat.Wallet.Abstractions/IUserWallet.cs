namespace Dtat.Wallet.Abstractions;

public interface IUserWallet<T> :
	SeedWork.IEntity<T>, SeedWork.IHashing<T> where T : struct
{
	T UserId { get; }

	T WalletId { get; }



	bool IsActive { get; }

	decimal Balance { get; }



	string? Description { get; }

	string? AdditionalData { get; }



	bool PaymentFeatureIsEnabled { get; }

	bool DepositeFeatureIsEnabled { get; }

	bool WithdrawFeatureIsEnabled { get; }

	bool TransferFeatureIsEnabled { get; }



	System.DateTime UpdateDateTime { get; }
}
