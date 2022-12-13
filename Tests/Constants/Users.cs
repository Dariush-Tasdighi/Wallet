namespace Tests.Constants;

internal class Users : object
{
	public Users() : base()
	{
	}

	// **************************************************
	public const string DariushIP = "192.168.1.100";

	public static Domain.User Dariush =
		new(cellPhoneNumber: "09121087461", displayName: "داریوش تصدیقی")
		{
			IsActive = true,
			NationalCode = "1234567891",
			EmailAddress = "dariusht@gmail.com",
		};
	// **************************************************

	// **************************************************
	public const string AlirezaIP = "192.168.1.100";

	public static Domain.User Alireza =
		new(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
		{
			IsActive = true,
			NationalCode = "1234567890",
			EmailAddress = "AliRezaAlavi@Gmail.com",
		};
	// **************************************************

	// **************************************************
	public const string RezaIP = "192.168.1.100";

	public static Domain.User Reza =
		new(cellPhoneNumber: "09215149218", displayName: "رضا قدیمی")
		{
			IsActive = true,
			NationalCode = "0123456789",
			EmailAddress = "RezaQadimi.ir@Gmail.com",
		};
	// **************************************************
}
