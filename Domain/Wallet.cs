using Dtat.Wallet.Abstractions;

namespace Domain
{
	public class Wallet : Seedwork.Entity, Dtat.Wallet.Abstractions.IWallet<long>
	{
		#region Constructor
		public Wallet(long companyId, string name) : base()
		{
			Name = name;
			CompanyId = companyId;
			Token = System.Guid.NewGuid();

			ValidIPs =
				new System.Collections.Generic.List<ValidIP>();

			InvalidIPs =
				new System.Collections.Generic.List<InvalidIP>();

			UserWallets =
				new System.Collections.Generic.List<UserWallet>();

			Transactions =
				new System.Collections.Generic.List<Transaction>();
		}
		#endregion /Constructor

		#region Properties

		public long CompanyId { get; private set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public virtual Company? Company { get; private set; }



		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Name)]
		public string Name { get; set; }



		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Hash)]
		public string? Hash { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
		public string? Description { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
		public string? AdditionalData { get; set; }



		public bool IsActive { get; set; }

		public System.Guid Token { get; set; }

		public System.DateTime UpdateDateTime { get; private set; }



		public bool PaymentFeatureIsEnabled { get; set; }

		public bool DepositeFeatureIsEnabled { get; set; }

		public bool WithdrawFeatureIsEnabled { get; set; }

		/// <summary>
		/// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
		/// </summary>
		//public bool TransferFeatureIsEnabled { get; set; }



		[System.Text.Json.Serialization.JsonIgnore]
		public virtual System.Collections.Generic.IList<ValidIP> ValidIPs { get; private set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public virtual System.Collections.Generic.IList<InvalidIP> InvalidIPs { get; private set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public virtual System.Collections.Generic.IList<UserWallet> UserWallets { get; private set; }

		[System.Text.Json.Serialization.JsonIgnore]
		public virtual System.Collections.Generic.IList<Transaction> Transactions { get; private set; }

		#endregion /Properties
	}
}
