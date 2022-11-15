namespace Dtos.Users
{
	public class DepositeRequestDto : object
	{
		public DepositeRequestDto() : base()
		{
		}

		public UserDto? User { get; set; }

		public decimal Amount { get; set; }

		public System.Guid WaletToken { get; set; }

		public int TimeDurationInMillisecond { get; set; }

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.ProviderName)]
		public string? ProviderName { get; set; }

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.ReferenceCode)]
		public string? ReferenceCode { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
		public string? UserDescription { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.Description)]
		public string? SystemicDescription { get; set; }

		[System.ComponentModel.DataAnnotations.MaxLength
			(length: Dtat.Wallet.Abstractions.Constant.MaxLength.AdditionalData)]
		public string? AdditionalData { get; set; }
	}
}
