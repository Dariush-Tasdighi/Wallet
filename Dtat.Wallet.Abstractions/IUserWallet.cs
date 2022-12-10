namespace Dtat.Wallet.Abstractions;

public interface IUserWallet<T> :
	SeedWork.IEntity<T>, SeedWork.IHasUpdateDateTime,
	SeedWork.IHasIsActive, SeedWork.IHashing where T : struct
{
	T UserId { get; }

	T WalletId { get; }



	decimal Balance { get; }



	string? Description { get; }

	string? AdditionalData { get; }
}
