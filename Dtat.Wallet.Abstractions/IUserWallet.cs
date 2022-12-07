namespace Dtat.Wallet.Abstractions;

public interface IUserWallet<T> :
	SeedWork.IEntity<T>, SeedWork.IHasIsActive<T>, SeedWork.IHashing<T> where T : struct
{
	T UserId { get; }

	T WalletId { get; }



	decimal Balance { get; }



	string? Description { get; }

	string? AdditionalData { get; }



	System.DateTime UpdateDateTime { get; }
}
