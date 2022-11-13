namespace Infrastructure
{
	public class ApplicationError : System.ApplicationException
	{
		public ApplicationError
			(long code, string message, System.Exception? innerException = null) :
			base(message: $"{code}: {message}", innerException: innerException)
		{
			Code = code;

			DisplayMessage =
				$"{code}: خطای داخلی سامانه!";

			//DisplayMessage =
			//	$"{code}: Internal Server Error!";
		}

		public long Code { get; }

		public string DisplayMessage { get; }
	}
}
