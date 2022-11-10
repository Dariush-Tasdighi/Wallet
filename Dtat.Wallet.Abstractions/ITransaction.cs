namespace Dtat.Wallet.Abstractions
{
	public interface ITransaction<T> : IBaseEntity<T>
	{
		T UserId { get; }

		T CompanyWalletId { get; }



		decimal Amount { get; }



		string? PaymentReferenceCode { get; }

		string? TransfererCompanyUserIdentity { get; }

		string? DepositeOrWithdrawProviderName { get; }

		string? DepositeOrWithdrawReferenceCode { get; }



		string? UserDescription { get; }

		string? SystemicDescription { get; }



		string Hash { get; }
	}
}
