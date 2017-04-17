namespace EventSourcing.Core.Domain
{
	/// <summary>
	/// Событие.
	/// </summary>
	public abstract class IEvent : IMessage
	{
		public int Version { get; set; }
	}
}
