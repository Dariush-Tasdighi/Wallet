namespace Dtat.Wallet.Abstractions.SeedWork;

public static class Constant : object
{
	static Constant()
	{
	}

	public static class MaxLength : object
	{
		static MaxLength()
		{
		}

		public const int IP = 15;

		/// <summary>
		/// For Hash 256
		/// </summary>
		public const int Hash = 64;

		public const int Name = 100;

		public const int DisplayName = 100;

		public const int NationalCode = 10;

		public const int ProviderName = 50;

		public const int Description = 1000;

		public const int EmailAddress = 255;

		public const int ReferenceCode = 100;

		public const int CellPhoneNumber = 14;

		public const int AdditionalData = 1000;
	}
}
