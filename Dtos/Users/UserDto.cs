namespace Dtos.Users
{
	public class UserDto : object
	{
		public UserDto() : base()
		{
		}

		#region IP (User IP)
		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.IP)]
		public string? IP { get; set; }
		#endregion /IP (User IP)

		#region DisplayName (Full Name)
		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.DisplayName)]
		public string? DisplayName { get; set; }
		#endregion /DisplayName (Full Name)

		#region CellPhoneNumber
		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.CellPhoneNumber)]
		public string? CellPhoneNumber { get; set; }
		#endregion /CellPhoneNumber

		#region EmailAddress
		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.EmailAddress)]
		public string? EmailAddress { get; set; }
		#endregion /EmailAddress

		#region NationalCode
		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.NationalCode)]
		public string? NationalCode { get; set; }
		#endregion /NationalCode

		#region AdditionalData
		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
		public string? AdditionalData { get; set; }
		#endregion /AdditionalData

		#region PaymentFeatureIsEnabled
		public bool PaymentFeatureIsEnabled { get; set; }
		#endregion /PaymentFeatureIsEnabled

		#region DepositeFeatureIsEnabled
		public bool DepositeFeatureIsEnabled { get; set; }
		#endregion /DepositeFeatureIsEnabled

		#region WithdrawFeatureIsEnabled
		public bool WithdrawFeatureIsEnabled { get; set; }
		#endregion /WithdrawFeatureIsEnabled

		/// <summary>
		/// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
		/// </summary>
		//public bool TransferFeatureIsEnabled { get; set; }
	}
}
