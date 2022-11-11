namespace Domain
{
	public class User : Seedwork.Entity, Dtat.Wallet.Abstractions.IUser<long>
	{
		#region Constructor
		public User(string cellPhoneNumber, string displayName) : base()
		{
			DisplayName = displayName;
			UpdateDateTime = InsertDateTime;
			CellPhoneNumber = cellPhoneNumber;

			UserWallets =
				new System.Collections.Generic.List<UserWallet>();

			Transactions =
				new System.Collections.Generic.List<Transaction>();
		}
		#endregion /Constructor

		#region Properties

		public string? Username { get; set; }

		public string? EmailAddress { get; set; }

		public string? NationalCode { get; set; }

		public string DisplayName { get; set; }

		public string CellPhoneNumber { get; set; }

		public System.DateTime UpdateDateTime { get; private set; }

		public string? Hash { get; private set; }

		public string? Description { get; set; }

		public virtual System.Collections.Generic.IList<UserWallet> UserWallets { get; private set; }

		public virtual System.Collections.Generic.IList<Transaction> Transactions { get; private set; }

		#endregion /Properties
	}
}
