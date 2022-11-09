namespace Dtat.Wallet.Abstractions
{
	public interface ITransaction<T> : IBaseEntity<T>
	{
		T UserId { get; }

		decimal Amount { get; }

		System.DateTime Timestamp { get; }



		string? PaymentReferenceCode { get; }

		string? TransfererApplicationUserId { get; }

		string? DepositeOrWithdrawProviderName { get; }

		string? DepositeOrWithdrawReferenceCode { get; }



		string? UserDescription { get; }

		string? SystemicDescription { get; }



		string Hash { get; }
	}
}
