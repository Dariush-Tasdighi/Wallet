using System.Linq;

namespace Server.Services;

public static class CompanyWalletsService : object
{
	static CompanyWalletsService()
	{
	}

	#region CheckAndGetCompanyWalletByTokens()
	public static Dtat.Result<Domain.CompanyWallet> CheckAndGetCompanyWalletByTokens
		(Data.DatabaseContext databaseContext, System.Guid companyToken, System.Guid walletToken)
	{
		var result =
			new Dtat.Result<Domain.CompanyWallet>();

		var companyWallet =
			databaseContext.CompanyWallets
			.Where(current => current.Wallet != null && current.Wallet.Token == walletToken)
			.Where(current => current.Company != null && current.Company.Token == companyToken)
			.FirstOrDefault();

		if (companyWallet == null)
		{
			var errorMessage =
				Resources.Messages.Errors.TheCompanyDoesNotHaveAccessToThisWallet;

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		if (companyWallet.IsActive == false)
		{
			var errorMessage =
				Resources.Messages.Errors.TheCompanyAccessToThisWalletIsNotActive;

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		result.Data =
			companyWallet;

		return result;
	}
	#endregion /CheckAndGetCompanyWalletByTokens()
}
