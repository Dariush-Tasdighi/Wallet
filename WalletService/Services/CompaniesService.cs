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
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.ThereIsNotAnyItemWithThisToken,
				arg0: nameof(company));

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		if (company.IsActive == false)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.TheItemIsNotActive,
				arg0: nameof(company));

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		result.Data = company;

		return result;
	}
	#endregion /CheckAndGetCompanyByToken()
}
