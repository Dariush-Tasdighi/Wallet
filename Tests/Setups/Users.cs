namespace Tests.Setups;

internal class Users : object
{
	public Users() : base()
	{
	}


	public class Dariush : Base.UserBase
	{
		internal static Dariush Instance
		{
			get
			{
				return new Dariush();
			}
		}

		private Dariush() : base()
		{
			IP = "192.168.1.100";

			User =
				new(cellPhoneNumber: Helpers.Utility.FakeCellPhoneNumber(), displayName: "داریوش تصدیقی")
				{
					IsActive = true,
					EmailAddress = Helpers.Utility.FakeEmailAddress(),
					NationalCode = Helpers.Utility.FakeNationalCode(),
				};
		}
	}

	public class Reza : Base.UserBase
	{
		internal static Reza Instance
		{
			get
			{
				return new Reza();
			}
		}

		private Reza() : base()
		{
			IP = "192.168.1.100";

			User =
				new(cellPhoneNumber: Helpers.Utility.FakeCellPhoneNumber(), displayName: "رضا قدیمی")
				{
					IsActive = true,
					EmailAddress = Helpers.Utility.FakeEmailAddress(),
					NationalCode = Helpers.Utility.FakeNationalCode(),
				};
		}
	}
}
