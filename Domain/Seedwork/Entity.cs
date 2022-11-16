using Dtat.Wallet.Abstractions.SeedWork;

namespace Domain.Seedwork;

public abstract class Entity : object,
	IEntity<long>
{
	#region Constructor
	public Entity() : base()
	{
		InsertDateTime = Utility.Now;
	}
	#endregion /Constructor

	#region Properties

	#region Id Property
	/// <summary>
	/// For Seed Data
	/// </summary>
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
	//public long Id { get; private set; }
	public long Id { get; set; }
	#endregion /Id Property

	#region InsertDateTime Property
	[System.ComponentModel.DataAnnotations.Schema.DatabaseGenerated
		(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None)]
	public System.DateTime InsertDateTime { get; private set; }
	#endregion /InsertDateTime Property

	#endregion /Properties
}
