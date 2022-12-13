namespace Tests.Constants;

internal class Users : object
{
	public Users() : base()
	{
	}

	// **************************************************
	public static Domain.User DariushTasdighi =
		new(cellPhoneNumber: "09121087461", displayName: "داریوش تصدیقی")
		{
			IsActive = true,
			NationalCode = "1234567891",
			EmailAddress = "dariusht@gmail.com",
		};
	// **************************************************

	// **************************************************
	public static Domain.User AlirezaAlavi =
		new(cellPhoneNumber: "09123456789", displayName: "Ali Reza Alavi")
		{
			IsActive = true,
			NationalCode = "1234567890",
			EmailAddress = "AliRezaAlavi@Gmail.com",
		};
	// **************************************************
}
