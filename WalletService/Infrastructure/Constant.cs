namespace Infrastructure;

public static class Constant : object
{
	static Constant()
	{
	}

	public const string DefaultRoute = "api/[controller]";

	public const string DefaultAdminRoute = "api/admin/[controller]";

	public static class Message : object
	{
		public const string LogError = "Error: {Message}";

		public const string NotFound = "آیتم مورد نظر یافت نشد!";
	}

	public static class ErrorMessage : object
	{
		public const string AmountIsZero = "مقدار نمی‌تواند صفر باشد!";
	}

	public static class ErrorCode : object
	{
		static ErrorCode()
		{
		}

		// **********
		public const long Applicatioin = 10000000000;
		// **********

		// **********
		public const long Root = Applicatioin + 101000000;

		public const long Admin = Applicatioin + 102000000;
		// **********

		// **********
		public const long UsersController = Root + 101000;

		public const long Root_UsersController_Refund = UsersController + 101;

		public const long Root_UsersController_Payment = UsersController + 102;

		public const long Root_UsersController_Deposite = UsersController + 103;

		public const long Root_UsersController_Withdraw = UsersController + 104;

		public const long Root_UsersController_GetBalance = UsersController + 105;

		public const long Root_UsersController_GetTransactionById = UsersController + 106;

		public const long Root_UsersController_GetLastTransactions = UsersController + 107;

		public const long Root_UsersController_GetUserTransactions = UsersController + 108;
		// **********

		// **********
		public const long Admin_CompaniesController = Admin + 101000;

		public const long Admin_CompaniesController_GetCompanyByIdAsync = Admin_CompaniesController + 101;

		public const long Admin_CompaniesController_GetAllCompaniesAsync = Admin_CompaniesController + 102;
		// **********

		// **********
		public const long Admin_WalletsController = Admin + 102000;

		public const long Admin_WalletsController_GetWalletByIdAsync = Admin_WalletsController + 101;

		public const long Admin_WalletsController_GetAllWalletsAsync = Admin_WalletsController + 102;

		public const long Admin_WalletsController_GetWalletsByCompanyIdAsync = Admin_WalletsController + 103;
		// **********

		// **********
		public const long Admin_UsersController = Admin + 103000;

		public const long Admin_UsersController_GetUserByIdAsync = Admin_UsersController + 101;

		public const long Admin_UsersController_GetAllUsersAsync = Admin_UsersController + 102;

		public const long Admin_UsersController_GetUsersByFiltersAsync = Admin_UsersController + 103;
		// **********

		// **********
		public const long Admin_TransactionsController = Admin + 104000;

		public const long Admin_TransactionsController_GetAllTransactionsAsync = Admin_TransactionsController + 101;

		public const long Admin_TransactionsController_GetTransactionByIdAsync = Admin_TransactionsController + 102;

		public const long Admin_TransactionsController_GetTransactionByHashAsync = Admin_TransactionsController + 103;

		public const long Admin_TransactionsController_GetTransactionsByUserIdAsync = Admin_TransactionsController + 104;

		public const long Admin_TransactionsController_GetTransactionsByFiltersAsync = Admin_TransactionsController + 105;

		public const long Admin_TransactionsController_GetTransactionsByWalletIdAsync = Admin_TransactionsController + 106;

		public const long Admin_TransactionsController_GetTransactionsByCompanyIdAsync = Admin_TransactionsController + 107;

		public const long Admin_TransactionsController_GetTransactionByPaymentReferenceCodeAsync = Admin_TransactionsController + 108;

		public const long Admin_TransactionsController_GetTransactionByDepositeOrWithdrawReferenceCodeAsync = Admin_TransactionsController + 109;
		// **********
	}
}
