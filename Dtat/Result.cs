namespace Dtat;

public class Result : object
{
	public Result() : base()
	{
		IsSuccess = true;

		ErrorMessages =
			new System.Collections.Generic.List<string>();

		SuccessMessages =
			new System.Collections.Generic.List<string>();
	}

	public bool IsSuccess { get; protected set; }

	public System.Collections.Generic.IList<string> ErrorMessages { get; }

	public System.Collections.Generic.IList<string> SuccessMessages { get; }

	public bool AddErrorMessages(string? message)
	{
		message =
			Dtat.Utility.FixText(text: message);

		if (message == null)
		{
			return false;
		}

		if (ErrorMessages.Contains(item: message))
		{
			return false;
		}

		IsSuccess = false;

		ErrorMessages.Add(item: message);

		return true;
	}

	public bool AddSuccessMessages(string? message)
	{
		message =
			Dtat.Utility.FixText(text: message);

		if (message == null)
		{
			return false;
		}

		if (SuccessMessages.Contains(item: message))
		{
			return false;
		}

		SuccessMessages.Add(item: message);

		return true;
	}
}

public class Result<T> : Result
{
	public Result() : base()
	{
	}

	public T? Data { get; set; }
}
