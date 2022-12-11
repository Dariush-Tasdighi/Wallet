namespace Dtos.Users.Base;

public class Pagination : object
{
	public Pagination() : base()
	{
		PageSize = 10;
	}

	public int PageSize { get; set; }

	public int PageIndex { get; set; }

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
}