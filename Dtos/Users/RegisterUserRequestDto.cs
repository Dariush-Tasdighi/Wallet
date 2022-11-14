namespace Dtos.Users
{
	public class RegisterUserRequestDto : object
	{
		public RegisterUserRequestDto() : base()
		{
		}

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: 15)]
		public string? UserIP { get; set; }

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

		bool PaymentFeatureIsEnabled { get; set; }

		bool DepositeFeatureIsEnabled { get; set; }

		bool WithdrawFeatureIsEnabled { get; set; }

		bool TransferFeatureIsEnabled { get; set; }
	}
}
