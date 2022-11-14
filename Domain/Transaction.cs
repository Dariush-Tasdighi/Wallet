namespace Domain
{
	public class Transaction : Seedwork.Entity, Dtat.Wallet.Abstractions.ITransaction<long>
	{
		#region Constructor
		public Transaction(long userId, long walletId,
			decimal amount, string userIP, string serverIP) : base()
		{
			Amount = amount;
			UserId = userId;
			UserIP = userIP;
			ServerIP = serverIP;
			WalletId = walletId;
		}
		#endregion /Constructor

		#region Properties

		public long UserId { get; private set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public virtual User? User { get; private set; }



		public long WalletId { get; private set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public virtual Wallet? Wallet { get; private set; }



		public decimal Amount { get; private set; }

		public int TimeDurationInMillisecond { get; set; }



		public string? PaymentReferenceCode { get; set; }

		public string? TransfererCompanyUserIdentity { get; set; }

		public string? DepositeOrWithdrawProviderName { get; set; }

		public string? DepositeOrWithdrawReferenceCode { get; set; }



		public string? Hash { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength(length: 15)]
		public string UserIP { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength(length: 15)]
		public string ServerIP { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength(length: 1000)]
		public string? AdditionalData { get; set; }

		public string? UserDescription { get; set; }

		public string? SystemicDescription { get; set; }
		#endregion /Properties
	}
}
