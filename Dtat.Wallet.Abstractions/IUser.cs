namespace Dtat.Wallet.Abstractions;

/// <summary>
/// شخص حقیقی / حقوقی
/// </summary>
public interface IUser<T> :
	SeedWork.IEntity<T>, SeedWork.IHasUpdateDateTime,
	SeedWork.IHasIsActive, SeedWork.IHashing where T : struct
{
	bool IsVerified { get; }



	/// <summary>
	/// این فیلد الزامی است
	/// </summary>
	string DisplayName { get; }

	/// <summary>
	/// این فیلد الزامی است و در کل سامانه باید منحصر به فرد باشد
	/// </summary>
	string CellPhoneNumber { get; }



	string? Description { get; }

	string? EmailAddress { get; }

	string? NationalCode { get; }



	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }

	// صرفا در جهت اطلاع
	//System.Collections.Generic.IList<ITransaction<T>> PartyTransactions { get; }
}
