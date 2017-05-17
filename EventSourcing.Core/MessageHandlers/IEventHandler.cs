using EventSourcing.Core.Domain;

namespace EventSourcing.Core.MessageHandlers
{
	/// <summary>
	/// Интерфейс обработчика событий.
	/// </summary>
	/// <typeparam name="TEventHandler">Тип события.</typeparam>
	public interface IEventHandler<TEventHandler> : IMessageHandler<TEventHandler> 
		where TEventHandler : IEvent
	{
	}
}
