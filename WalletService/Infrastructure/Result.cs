namespace Infrastructure;

public class Result<T> : object
{
	public Result() : base()
	{
		IsSuccess = true;

		ErrorMessages =
			new System.Collections.Generic.List<string>();
	}

	public T? Data { get; set; }

	public bool IsSuccess { get; protected set; }

	public System.Collections.Generic.IList<string> ErrorMessages { get; }

	public bool AddErrorMessages(string? errorMessage)
	{
		errorMessage =
			Dtat.Utility.FixText(text: errorMessage);

		if (errorMessage == null)
		{
			return false;
		}

		if (ErrorMessages.Contains(item: errorMessage))
		{
			return false;
		}

		IsSuccess = false;

		ErrorMessages.Add(item: errorMessage);

		return true;
	}
}
