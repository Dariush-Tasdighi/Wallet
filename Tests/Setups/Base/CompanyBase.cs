namespace Tests.Setups.Base;

internal abstract class CompanyBase : object
{
	protected CompanyBase() : base()
	{
		IP = string.Empty;

		Company =
			new(name: string.Empty);
	}

	public string IP { get; protected set; }

	public System.Guid Token { get; protected set; }

	public Domain.Company Company { get; protected set; }
}
