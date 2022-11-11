namespace Domain
{
	public class Transaction : Seedwork.Entity, Dtat.Wallet.Abstractions.ITransaction<long>
	{
		#region Constructor
		public Transaction(long userId, long walletId, decimal amount) : base()
		{
			Amount = amount;
			UserId = userId;
			WalletId = walletId;
		}
		#endregion /Constructor

		#region Properties

		public long UserId { get; private set; }

		public virtual User? User { get; private set; }



		public long WalletId { get; private set; }

		public virtual Wallet? Wallet { get; private set; }



		public decimal Amount { get; private set; }



		public string? PaymentReferenceCode { get; set; }

		public string? TransfererCompanyUserIdentity { get; set; }

		public string? DepositeOrWithdrawProviderName { get; set; }

		public string? DepositeOrWithdrawReferenceCode { get; set; }



		public string? Hash { get; set; }

		public string? UserDescription { get; set; }

		public string? SystemicDescription { get; set; }

		#endregion /Properties
	}
}
