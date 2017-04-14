using EventSourcing.Core.Domain;

namespace EventSourcing.Core.MessageHandlers
{
	/// <summary>
	/// Интерфейс обработчика событий.
	/// </summary>
	/// <typeparam name="T">Тип события.</typeparam>
	public interface IEventHandler<T> : IMessageHandler<T> where T : IEvent
	{
	}
}
