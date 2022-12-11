using System.Linq;

namespace Server.Services;

public static class UsersService : object
{
	static UsersService()
	{
	}

	#region CreateOrUpdateUser()
	public static Domain.User CreateOrUpdateUser
		(Data.DatabaseContext databaseContext,
		string cellPhoneNumber, string displayName,
		string? emailAddress, string? nationalCode)
	{
		var user =
			databaseContext.Users
			.Where(current => current.CellPhoneNumber == cellPhoneNumber)
			.FirstOrDefault();

		if (user == null)
		{
			user =
				new Domain.User(cellPhoneNumber: cellPhoneNumber, displayName: displayName)
				{
					IsActive = true,

					DisplayName = displayName,
					EmailAddress = emailAddress,
					NationalCode = nationalCode,
				};

			databaseContext.Add(entity: user);
		}
		else
		{
			user.DisplayName = displayName;
			user.EmailAddress = emailAddress;
			user.NationalCode = nationalCode;
		}

		user.UpdateHash();

		// دستور ذیل باید نوشته شود Id به دلیل نوع
		databaseContext.SaveChanges();

		return user;
	}
	#endregion /CreateOrUpdateUser()

	#region CheckAndGetUserByCellPhoneNumber()
	public static Dtat.Result<Domain.User>
		CheckAndGetUserByCellPhoneNumber
		(Data.DatabaseContext databaseContext, string cellPhoneNumber)
	{
		var result =
			new Dtat.Result<Domain.User>();

		var user =
			databaseContext.Users
			.Where(current => current.CellPhoneNumber == cellPhoneNumber)
			.FirstOrDefault();

		if (user == null)
		{
			var errorMessage =
				Resources.Messages.Errors.NoUserWithThisCellPhoneNumber;

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		var hashValidation =
			user.CheckHashValidation();

		if (hashValidation == false)
		{
			var errorMessage =
				Resources.Messages.Errors.InconsitencyDataForUser;

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		if (user.IsActive == false)
		{
			var errorMessage = string.Format
				(format: Resources.Messages.Errors.TheItemIsNotActive,
				arg0: nameof(Domain.User));

			result.AddErrorMessages
				(message: errorMessage);

			return result;
		}

		result.Data = user;

		return result;
	}
	#endregion /CheckAndGetUserByCellPhoneNumber()
}
