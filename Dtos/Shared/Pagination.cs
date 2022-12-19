namespace Dtos.Shared;

public abstract class Pagination : object
{
	#region Constructor
	public Pagination() : base()
	{
		PageSize = 10;
	}
	#endregion /Constructor

	#region Properties

	#region PageSize
	public int PageSize { get; set; }
	#endregion /PageSize

	#region PageIndex
	public int PageIndex { get; set; }
	#endregion PageIndex

	#region Skip
	public int Skip
	{
		get
		{
			if (PageIndex < 0 || PageSize < 0)
			{
				return 0;
			}

			var result =
				(PageIndex - 1) * PageSize;

			return result;
		}
	}
	#endregion /Skip

	#endregion /Properties
}
