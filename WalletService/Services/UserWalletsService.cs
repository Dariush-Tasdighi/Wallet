using System.Linq;

namespace Server.Services;

public static class UserWalletsService : object
{
	static UserWalletsService()
	{
	}

	#region CreateOrUpdateUserWallet()
	public static Domain.UserWallet
		CreateOrUpdateUserWallet
		(Data.DatabaseContext databaseContext,
		long userId, long walletId, string? additionalData)
	{
		var userWallet =
			databaseContext.UserWallets
			.Where(current => current.UserId == userId)
			.Where(current => current.WalletId == walletId)
			.FirstOrDefault();

		if (userWallet == null)
		{
			userWallet =
				new Domain.UserWallet(userId: userId, walletId: walletId)
				{
					IsActive = true,

					AdditionalData = additionalData,
				};

			databaseContext.Add(entity: userWallet);
		}
		else
		{
			userWallet.AdditionalData = additionalData;
		}

		userWallet.UpdateHash();

		// دستور ذیل باید نوشته شود Id به دلیل نوع
		databaseContext.SaveChanges();

		return userWallet;
	}
	#endregion /CreateOrUpdateUserWallet()

	#region CheckAndGetUserWallet()
	public static Dtat.Result<Domain.UserWallet> CheckAndGetUserWallet
		(Data.DatabaseContext databaseContext, string cellPhoneNumber, System.Guid walletToken)
	{
		var result =
			new Dtat.Result<Domain.UserWallet>();

		var userWallet =
			databaseContext.UserWallets
			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
			.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
			.FirstOrDefault();

		if (userWallet == null)
		{
			var errorMessage =
				Resources.Messages.Errors.TheUserDoesNotHaveAccessToThisWallet;

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		var hashValidation =
			userWallet.CheckHashValidation();

		if (hashValidation == false)
		{
			var errorMessage =
				Resources.Messages.Errors.InconsitencyDataForUserWallet;

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		if (userWallet.IsActive == false)
		{
			var errorMessage =
				Resources.Messages.Errors.TheUserAccessToThisWalletIsNotActive;

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		result.Data = userWallet;

		return result;
	}
	#endregion /CheckAndGetUserWallet()

	#region GetUserBalanceWithCheckingDataConsistency()
	public static Dtat.Result<decimal> GetUserBalanceWithCheckingDataConsistency
		(Data.DatabaseContext databaseContext, System.Guid walletToken,
		string cellPhoneNumber, Domain.UserWallet userWallet)
	{
		var result =
			new Dtat.Result<decimal>();

		// **************************************************
		// نوع اول
		// بررسی معتبر بودن مقدار مانده کاربر
		// در کیف پول بر اساس هش موجود در رکورد
		// **************************************************
		// نکته مهم: نیازی به دستورات ذیل نیست، این دستورات
		// در دستورات قبلی چک می‌شود
		// **************************************************
		//var hashValidation =
		//	userWallet.CheckHashValidation();

		//if (hashValidation == false)
		//{
		//	var errorMessage =
		//		$"There is inconsitency data (Type 1) in user wallet balance!";

		//	result.AddErrorMessages(message: errorMessage);

		//	return result;
		//}
		// **************************************************

		// **************************************************
		// نوع دوم
		// بررسی معتبر بودن مقدار مانده کاربر در کیف پول
		// بر اساس کلیه تراکنش‌های کاربر در کیف پول مربوطه
		// **************************************************
		var balance =
			databaseContext.Transactions
			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
			.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
			.Sum(current => current.Amount);

		if (balance != userWallet.Balance)
		{
			var errorMessage =
				Resources.Messages.Errors.InconsitencyDataForUserBalance;

			result.AddErrorMessages(message: errorMessage);

			return result;
		}
		// **************************************************

		result.Data =
			userWallet.Balance;

		return result;
	}
	#endregion /GetUserBalanceWithCheckingDataConsistency()

	#region GetUserWithdrawBalance()
	public static Dtat.Result<decimal> GetUserWithdrawBalance
		(Data.DatabaseContext databaseContext,
		System.Guid walletToken, string cellPhoneNumber, Infrastructure.IUtility utility)
	{
		var result =
			new Dtat.Result<decimal>();

		var now =
			utility.GetNow().Date;

		// همه خروج و برداشت مبالغ
		var allAntiDeposite =
			databaseContext.Transactions
			.Where(current => current.Amount < 0)
			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
			.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
			.Sum(current => current.Amount);

		// همه مبالغ قابل برداشت تا تاریخ مذکور
		var allDeposite =
			databaseContext.Transactions
			.Where(current => current.Amount > 0)
			.Where(current => current.WithdrawDate != null && current.WithdrawDate <= now)
			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
			.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
			.Sum(current => current.Amount);

		// کل مبالغ قابل برداشت
		// منفی است باید از بعلاوه استفاده شود allAntiDeposite باید دقت داشته باشیم که چون
		var withdrawBalance =
			allDeposite + allAntiDeposite;

		if (withdrawBalance < 0)
		{
			withdrawBalance = 0;
		}

		result.Data =
			withdrawBalance;

		return result;
	}
	#endregion /GetUserWithdrawBalance()

	#region CheckUserRefundBalance()
	public static Dtat.Result<decimal> GetUserRefundableBalance
		(Data.DatabaseContext databaseContext, long transactionId,
		System.Guid walletToken, string cellPhoneNumber, Infrastructure.IUtility utility)
	{
		var result =
			new Dtat.Result<decimal>();

		var now =
			utility.GetNow().Date;

		var foundedPayment =
			databaseContext.Transactions
			.Where(current => current.Id == transactionId)
			.Where(current => current.Type == Dtat.Wallet.Abstractions.SeedWork.TransactionType.Payment)
			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
			.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
			.FirstOrDefault();

		if (foundedPayment == null)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.TheItemIsNull,
				arg0: nameof(foundedPayment));

			result.AddErrorMessages(message: errorMessage);

			return result;
		}

		var allRefunds =
			databaseContext.Transactions
			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
			.Where(current => current.User != null && current.User.CellPhoneNumber == cellPhoneNumber)
			.Where(current => current.ParentTransactionId == transactionId)
			.Sum(current => current.Amount);

		// Payment مبلغ باقی مانده از تراکنش
		var balance =
			(foundedPayment.Amount * - 1) + (allRefunds * - 1);

		result.Data = balance;

		return result;
	}
	#endregion /CheckUserRefundBalance()
}
