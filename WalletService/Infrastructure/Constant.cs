namespace Infrastructure;

public static class Constant : object
{
	static Constant()
	{
	}

	//public const string DefaultRoute = "[controller]";

	public const string DefaultRoute = "api/[controller]";

	public const string DefaultAdminRoute = "api/admin/[controller]";

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

		public const long Applicatioin = 10000000000;

		public const long Admin = Applicatioin + 100000000;

		// **********
		public const long Admin_CompaniesController = Admin + 100000;

		public const long Admin_CompaniesController_GetCompanyByIdAsync = Admin_CompaniesController + 100;

		public const long Admin_CompaniesController_GetAllCompaniesAsync = Admin_CompaniesController + 101;
		// **********

		// **********
		public const long Admin_WalletsController = Admin + 100000;

		public const long Admin_WalletsController_GetWalletByIdAsync = Admin_WalletsController + 100;

		public const long Admin_WalletsController_GetAllWalletsAsync = Admin_WalletsController + 101;

		public const long Admin_WalletsController_GetWalletsByCompanyIdAsync = Admin_WalletsController + 102;
		// **********
	}
}
