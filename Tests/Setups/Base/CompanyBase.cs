namespace Tests.Setups.Base;

internal abstract class CompanyBase : object
{
	protected CompanyBase() : base()
	{
		ServerIP = string.Empty;

		Company =
			new(name: string.Empty);
	}

	public string ServerIP { get; protected set; }

	public System.Guid Token { get; protected set; }

	public Domain.Company Company { get; protected set; }
}
