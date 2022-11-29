namespace Dtos.Users;

public class GetBalanceRequestDto : object
{
	#region Constructor
	public GetBalanceRequestDto() : base()
	{
		User = new();
	}
	#endregion /Constructor

	#region Properties

	#region User
	[System.ComponentModel.DataAnnotations.Required]
	public GetBalanceRequestUserDto User { get; set; }
	#endregion /User

	#region WaletToken
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid WaletToken { get; set; }
	#endregion /WaletToken

	#region CompanyToken
	[System.ComponentModel.DataAnnotations.Required]
	public System.Guid CompanyToken { get; set; }
	#endregion /CompanyToken

	#endregion /Properties
}
