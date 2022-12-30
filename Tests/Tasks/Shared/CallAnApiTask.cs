namespace Tests.Tasks.Shared;

public abstract class CallAnApiTask<TController> : object
{
	#region Constructor(s)
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public CallAnApiTask(string ip) : base()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	{
		// **************************************************
		MockLogger =
			new Moq.Mock<Microsoft.Extensions.Logging.ILogger
			<TController>>();
		// **************************************************

		// **************************************************
		MockUtility =
			new Moq.Mock<Infrastructure.IUtility>();

		MockUtility.Setup(current => current
			.GetServerIP(Moq.It.IsAny<Microsoft.AspNetCore.Http.HttpRequest>()))
			.Returns(value: ip);
		// **************************************************
	}
	#endregion /Constructor(s)

	#region Property(ies)
	protected TController Controller { get; init; }

	protected Moq.Mock<Infrastructure.IUtility> MockUtility { get; }

	protected Moq.Mock<Microsoft.Extensions.Logging.ILogger<TController>> MockLogger { get; }
	#endregion /Property(ies)
}
