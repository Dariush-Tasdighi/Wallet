namespace Domain.Seedwork;

public abstract class Entity : object,
	Dtat.Wallet.Abstractions.SeedWork.IEntity<long>
{
	#region Constructor
	public Entity() : base()
	{
		InsertDateTime =
			Dtat.Utility.Now;
	}
	#endregion /Constructor

	#region Properties

	#region Id
	/// <summary>
	/// شناسه رکورد - شناسه اطلاعات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
	public long Id { get; set; }
	#endregion /Id

	#region InsertDateTime
	/// <summary>
	/// زمان ثبت (درج) اطلاعات
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated(databaseGeneratedOption:
		System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTime InsertDateTime { get; private set; }
	#endregion /InsertDateTime

	#endregion /Properties
}
