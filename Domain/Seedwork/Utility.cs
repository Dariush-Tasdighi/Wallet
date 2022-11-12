namespace Domain.Seedwork;

public static class Utility : object
{
	static Utility()
	{
	}

	public static System.DateTime Now
	{
		get
		{
			var result =
				System.DateTime.Now;

			return result;
		}
	}
}
