using System.Linq;

namespace Server.Services;

public static class ValidIPsService : object
{
	static ValidIPsService()
	{
	}

	#region CheckServerIPByCompanyToken()
	public static Dtat.Result CheckServerIPByCompanyToken
		(Data.DatabaseContext databaseContext, string serverIP, System.Guid companyToken,
		System.Guid? walletToken, string? cellPhoneNumber, Infrastructure.IUtility utility)
	{
		var result =
			new Dtat.Result();

		var validIP =
			databaseContext.ValidIPs
			.Where(current => current.ServerIP == serverIP)
			.Where(current => current.Company != null && current.Company.Token == companyToken)
			.FirstOrDefault();

		if (validIP == null)
		{
			// **************************************************
			var invalidRequestLog =
				new Domain.InvalidRequestLog(serverIP: serverIP)
				{
					//Id
					//ServerIP
					//Description
					//InsertDateTime

					WalletToken = walletToken,
					CompanyToken = companyToken,
					CellPhoneNumber = cellPhoneNumber,
				};

			databaseContext.Add(entity: invalidRequestLog);

			databaseContext.SaveChanges();
			// **************************************************

			// **************************************************
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.ThisIPIsNotDefinedForThisCompany
				, arg0: serverIP);

			result.AddErrorMessages
				(message: errorMessage);

			return result;
			// **************************************************
		}

		var now =
			utility.GetNow();

		validIP.TotalRequestCount++;

		if (validIP.LastRequestDateTime.HasValue == false)
		{
			validIP.CurrentDayRequestCount = 1;
		}
		else
		{
			if (now.Date <= validIP.LastRequestDateTime.Value.Date)
			{
				validIP.CurrentDayRequestCount++;
			}
			else
			{
				validIP.PreviousDay6RequestCount = validIP.PreviousDay5RequestCount;
				validIP.PreviousDay5RequestCount = validIP.PreviousDay4RequestCount;
				validIP.PreviousDay4RequestCount = validIP.PreviousDay3RequestCount;
				validIP.PreviousDay3RequestCount = validIP.PreviousDay2RequestCount;
				validIP.PreviousDay2RequestCount = validIP.PreviousDay1RequestCount;
				validIP.PreviousDay1RequestCount = validIP.CurrentDayRequestCount;

				validIP.CurrentDayRequestCount = 1;
			}
		}

		validIP.LastRequestDateTime = now;

		databaseContext.SaveChanges();

		if (validIP.IsActive == false)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.ThisIPIsNotActive
				, arg0: serverIP);

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		return result;
	}
	#endregion /CheckServerIPByCompanyToken()
}
