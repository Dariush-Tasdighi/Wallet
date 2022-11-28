using System.Linq;

namespace Server.Services;

public static class UserWalletsService : object
{
	static UserWalletsService()
	{
	}

	#region CreateOrUpdateUserWallet()
	public static Domain.UserWallet
		CreateOrUpdateUserWallet(Data.DatabaseContext databaseContext,
		long userId, long walletId, bool paymentFeatureIsEnabled,
		bool depositeFeatureIsEnabled, bool withdrawFeatureIsEnabled,
		bool transferFeatureIsEnabled, string? additionalData)
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

					PaymentFeatureIsEnabled = paymentFeatureIsEnabled,
					DepositeFeatureIsEnabled = depositeFeatureIsEnabled,
					WithdrawFeatureIsEnabled = withdrawFeatureIsEnabled,
					TransferFeatureIsEnabled = transferFeatureIsEnabled,
				};

			databaseContext.Add(entity: userWallet);
		}
		else
		{
			userWallet.AdditionalData = additionalData;

			userWallet.PaymentFeatureIsEnabled = paymentFeatureIsEnabled;
			userWallet.DepositeFeatureIsEnabled = depositeFeatureIsEnabled;
			userWallet.WithdrawFeatureIsEnabled = withdrawFeatureIsEnabled;
			userWallet.TransferFeatureIsEnabled = transferFeatureIsEnabled;
		}

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
				$"This user does not have any access to this wallet!";

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		if (userWallet.IsActive == false)
		{
			var errorMessage =
				$"Access of this user to the wallet is not active!";

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		result.Data = userWallet;

		return result;
	}
	#endregion /CheckAndGetUserWallet()
}
