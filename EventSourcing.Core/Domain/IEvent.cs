namespace EventSourcing.Core.Domain
{
	/// <summary>
	/// Событие.
	/// </summary>
	public interface IEvent : IMessage
	{
		int Version { get; set; }
	}
}
