using System.Linq;

namespace Server.Services;

public static class ValidIPsService : object
{
	static ValidIPsService()
	{
	}

	#region CheckAndGetCompanyByToken()
	public static Dtat.Result CheckServerIPByCompanyToken
		(Data.DatabaseContext databaseContext, string serverIP, System.Guid companyToken,
		System.Guid? walletToken, string? cellPhoneNumber)
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

			var errorMessage =
				$"This server IP {serverIP} is not defined for this company!";

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		var now =
			Dtat.Utility.Now;

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
			var errorMessage =
				$"This server IP {serverIP} is not active for this company!";

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		return result;
	}
	#endregion /CheckAndGetCompanyByToken()
}
