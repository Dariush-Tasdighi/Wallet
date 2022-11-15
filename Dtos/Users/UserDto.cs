namespace Dtos.Users
{
	public class UserDto : object
	{
		public UserDto() : base()
		{
		}

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.IP)]
		public string? IP { get; set; }

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.DisplayName)]
		public string? DisplayName { get; set; }

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.CellPhoneNumber)]
		public string? CellPhoneNumber { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.EmailAddress)]
		public string? EmailAddress { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.NationalCode)]
		public string? NationalCode { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
		public string? AdditionalData { get; set; }

		public bool PaymentFeatureIsEnabled { get; set; }

		public bool DepositeFeatureIsEnabled { get; set; }

		public bool WithdrawFeatureIsEnabled { get; set; }

		/// <summary>
		/// فعلا در این فاز انتقال به غیر طراحی و پیاده‌سازی نشده است
		/// </summary>
		//public bool TransferFeatureIsEnabled { get; set; }
	}
}
