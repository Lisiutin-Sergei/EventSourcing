using EventSourcing.Core.Domain;
using System.Collections.Generic;

namespace EventSourcing.Data.EventStore
{
	/// <summary>
	/// Интерфейс хранилища событий.
	/// </summary>
	public interface IEventStore
	{
		/// <summary>
		/// Сохранить события.
		/// </summary>
		/// <param name="aggregateId">Идентификатор сущности.</param>
		/// <param name="events">События.</param>
		/// <param name="expectedVersion">Ожидаемая версия.</param>
		void SaveEvents(int aggregateId, IEnumerable<IEvent> events, int expectedVersion);

		/// <summary>
		/// Получить список событий для сущности.
		/// </summary>
		/// <param name="aggregateId">Идентификатор сущности.</param>
		/// <returns>Список событий.</returns>
		List<IEvent> GetEventsForAggregate(int aggregateId);
	}
}
