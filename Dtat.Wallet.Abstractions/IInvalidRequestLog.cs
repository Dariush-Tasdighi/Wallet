namespace Dtat.Wallet.Abstractions;

public interface IInvalidRequestLog<T> :
	SeedWork.IEntity<T> where T : struct
{
	string ServerIP { get; }

	string? Description { get; }



	string? CellPhoneNumber { get; }

	System.Guid? WalletToken { get; }

	System.Guid? CompanyToken { get; }
}
