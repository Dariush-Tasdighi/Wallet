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

		// دستور ذیل باید نوشته شود Id به دلیل نوع
		databaseContext.SaveChanges();

		return user;
	}
	#endregion /CreateOrUpdateUser()
}
