namespace Dtat.Wallet.Abstractions
{
	public interface ITransaction<T, P>
	{
		T Id { get; set; }

		P UserId { get; set; }

		decimal Amount { get; set; }

		System.DateTime Timestamp { get; set; }



		string? PaymentReferenceCode { get; set; }

		string? TransfererApplicationUserId { get; set; }

		string? DepositeOrWithdrawProviderName { get; set; }

		string? DepositeOrWithdrawReferenceCode { get; set; }



		string? UserDescription { get; set; }

		string? SystemicDescription { get; set; }



		string Hash { get; set; }
	}
}
