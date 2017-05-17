using EventSourcing.Core.Domain;

namespace EventSourcing.Core.ServiceBus
{
	/// <summary>
	/// Интерфейс для публикации событий.
	/// </summary>
	public interface IEventPublisher
	{
		/// <summary>
		/// Опубликовать событие.
		/// </summary>
		/// <typeparam name="TEvent">Тип события.</typeparam>
		/// <param name="event">Событие.</param>
		void Publish<TEvent>(TEvent @event) where TEvent : IEvent;
	}
}
