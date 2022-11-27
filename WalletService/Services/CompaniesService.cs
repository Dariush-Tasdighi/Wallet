using System.Linq;

namespace Server.Services;

public static class CompaniesService : object
{
	static CompaniesService()
	{
	}

	#region CheckAndGetCompanyByToken()
	public static Dtat.Result<Domain.Company>
		CheckAndGetCompanyByToken(Data.DatabaseContext databaseContext, System.Guid token)
	{
		var result =
			new Dtat.Result<Domain.Company>();

		var company =
			databaseContext.Companies
			.Where(current => current.Token == token)
			.FirstOrDefault();

		if (company == null)
		{
			var errorMessage =
				$"There is no any company with this token!";

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		if (company.IsActive == false)
		{
			var errorMessage =
				$"This company is not active!";

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		result.Data = company;

		return result;
	}
	#endregion /CheckAndGetCompanyByToken()
}
