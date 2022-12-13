using Newtonsoft.Json.Linq;
using System.Linq;

namespace Tests.Helpers;

internal static class Utility : object
{
	static Utility()
	{
	}

	internal static void DetachAllEntities
		(this Microsoft.EntityFrameworkCore.DbContext databaseContext)
	{
		var changedEntriesCopy = databaseContext.ChangeTracker.Entries()
			.Where(current =>
				current.State == Microsoft.EntityFrameworkCore.EntityState.Added ||
				current.State == Microsoft.EntityFrameworkCore.EntityState.Modified ||
				current.State == Microsoft.EntityFrameworkCore.EntityState.Deleted)
			.ToList();

		foreach (var entry in changedEntriesCopy)
		{
			entry.State =
				Microsoft.EntityFrameworkCore.EntityState.Detached;
		}
	}

	internal static string ReferenceCode
	{
		get
		{
			var referenceCode =
				Faker.RandomNumber.Next(min: 1000000000, max: 9999999999)
				.ToString();

			return referenceCode;
		}
	}

	internal static string FakeNationalCode()
	{
		var value =
			Faker.RandomNumber.Next(min: 1000000000, max: 9999999999)
			.ToString();

		return value;
	}

	internal static string FakeCellPhoneNumber()
	{
		var value =
			Faker.RandomNumber.Next(min: 10000000000, max: 99999999999)
			.ToString(); ;

		return value;
	}

	internal static string FakeEmailAddress()
	{
		var value =
			Faker.Internet.Email();

		return value;
	}
}
