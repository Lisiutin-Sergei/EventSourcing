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
		/// <typeparam name="T">Тип события.</typeparam>
		/// <param name="event">Событие.</param>
		void Publish<T>(T @event) where T : IEvent;
	}
}
