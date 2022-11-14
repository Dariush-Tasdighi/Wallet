namespace Dtos.Users
{
	public class UserDto : object
	{
		public UserDto() : base()
		{
		}

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 15)]
		public string? IP { get; set; }

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 100)]
		public string? DisplayName { get; set; }

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 14)]
		public string? CellPhoneNumber { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 50)]
		public string? CompanyUserIdentity { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 100)]
		public string? EmailAddress { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 10)]
		public string? NationalCode { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 1000)]
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
