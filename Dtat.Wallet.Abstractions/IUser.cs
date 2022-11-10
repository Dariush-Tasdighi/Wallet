﻿namespace Dtat.Wallet.Abstractions
{
	/// <summary>
	/// شخص حقیقی / حقوقی
	/// </summary>
	public interface IUser<T> : IBaseEntity<T>
	{
		string? Username { get; }

		string? DisplayName { get; }

		string? EmailAddress { get; }

		string? NationalCode { get; }

		/// <summary>
		/// این فیلد الزامی است
		/// </summary>
		string CellPhoneNumber { get; }

		System.DateTime UpdateDateTime { get; }



		string Hash { get; }



		string? Description { get; }



		System.Collections.Generic.IList<IUserWallet<T>> UserWallets { get; }

		System.Collections.Generic.IList<ITransaction<T>> Transactions { get; }
	}
}
