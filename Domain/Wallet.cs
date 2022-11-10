namespace Domain
{
	public class Wallet : Seedwork.Entity, Dtat.Wallet.Abstractions.IWallet<long>
	{
		#region Constructor
		public Wallet(string name, string displayName)
		{
			Name = name;
			DisplayName = displayName;
			Token = System.Guid.NewGuid();

			UserWallets =
				new System.Collections.Generic.List<UserWallet>();

			Transactions =
				new System.Collections.Generic.List<Transaction>();
		}
		#endregion /Constructor

		#region Properties

		public long CompanyId { get; set; }

		public string Name { get; set; }

		public string DisplayName { get; set; }

		public bool IsActive { get; set; }

		public string? ValidIPs { get; set; }

		public System.Guid Token { get; set; }

		public System.DateTime UpdateDateTime { get; private set; }

		public bool PaymentFeatureIsEnabled { get; set; }

		public bool DepositeFeatureIsEnabled { get; set; }

		public bool WithdrawFeatureIsEnabled { get; set; }

		public bool TransferFeatureIsEnabled { get; set; }

		public System.Collections.Generic.IList<UserWallet> UserWallets { get; private set; }

		public System.Collections.Generic.IList<Transaction> Transactions { get; private set; }

		#endregion /Properties
	}
}
