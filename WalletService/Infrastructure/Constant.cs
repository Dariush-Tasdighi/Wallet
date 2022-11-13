namespace Infrastructure;

public static class Constant : object
{
	static Constant()
	{
	}

	//public const string DefaultRoute = "[controller]";

	public const string DefaultRoute = "api/[controller]";

	public static class Message : object
	{
		public const string LogError = "Error: {Message}";

		public const string NotFound = "آیتم مورد نظر یافت نشد!";
	}

	public static class ErrorCode : object
	{
		static ErrorCode()
		{
		}

		public const int Applicatioin = 100000000;

		public const int CompaniesController = Applicatioin + 100000;

		public const int CompaniesController_GetCompanyByIdAsync = CompaniesController + 100;

		public const int CompaniesController_GetCompanyById = CompaniesController + 101;
	}
}
