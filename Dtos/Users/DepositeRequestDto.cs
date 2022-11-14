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

		public string? ProviderName { get; set; }

		public string? ReferenceCode { get; set; }

		public string? UserDescription { get; set; }

		public string? SystemicDescription { get; set; }
	}
}
