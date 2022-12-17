namespace Tests.Setups;

internal class Company : object
{
	public Company() : base()
	{
	}

	internal class Hit : Base.CompanyBase
	{
		internal static Hit Instance
		{
			get
			{
				return new Hit();
			}
		}

		private Hit() : base()
		{
			ServerIP = "192.168.1.110";

			Token = System.Guid.NewGuid();

			Company = new(name: "شرکت داد و ستد هستی")
			{
				IsActive = true,
			};
		}
	}
}
