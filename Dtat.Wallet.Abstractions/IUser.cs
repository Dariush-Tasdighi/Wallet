﻿namespace Dtat.Wallet.Abstractions
{
	/// <summary>
	/// شخص حقیقی / حقوقی
	/// </summary>
	public interface IUser<T> : IBaseEntity<T>
	{
		bool IsActive { get; }



		/// <summary>
		/// این فیلد الزامی است
		/// </summary>
		string DisplayName { get; }

		/// <summary>
		/// این فیلد الزامی است و در کل سامانه باید منحصر به فرد باشد
		/// </summary>
		string CellPhoneNumber { get; }



		string? Hash { get; }

		string? Description { get; }

		string? EmailAddress { get; }

		string? NationalCode { get; }

		string? AdditionalData { get; }



		System.DateTime UpdateDateTime { get; }



		// صرفا در جهت اطلاع
		//System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

		// صرفا در جهت اطلاع
		//System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }
	}
}
