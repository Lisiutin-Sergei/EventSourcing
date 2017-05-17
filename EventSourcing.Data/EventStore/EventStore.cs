using EventSourcing.Core.Domain;
using EventSourcing.Core.ServiceBus;
using EventSourcing.Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcing.Data.EventStore
{
	/// <summary>
	/// Хранилище событий.
	/// </summary>
	public class EventStore : IEventStore
	{
		/// <summary>
		/// Событие в хранилище.
		/// </summary>
		private struct EventDescriptor
		{
			public readonly IEvent EventData;
			public readonly int Id;
			public readonly int Version;

			public EventDescriptor(int id, IEvent eventData, int version)
			{
				Argument.NotNull(eventData, "Не задано событие.");

				EventData = eventData;
				Version = version;
				Id = id;
			}
		}

		/// <summary>
		/// Паблишер событий.
		/// </summary>
		private readonly IEventPublisher _publisher;

		/// <summary>
		/// Хранилище событий. Словарь(идентификатор сущности, список событий).
		/// </summary>
		private readonly Dictionary<int, List<EventDescriptor>> _events = new Dictionary<int, List<EventDescriptor>>();
		
		public EventStore(IEventPublisher publisher)
		{
			Argument.NotNull(publisher, "Не задан паблишер событий.");
			_publisher = publisher;
		}

		/// <summary>
		/// Сохранить события в хранилище.
		/// </summary>
		/// <param name="aggregateId">Идентификатор сущности.</param>
		/// <param name="events">Список событий.</param>
		/// <param name="expectedVersion">Ожидаемая версия.</param>
		public void SaveEvents(int aggregateId, IEnumerable<IEvent> events, int expectedVersion)
		{
			Argument.NotNull(events, "Не задан список событий.");
			Argument.Require(aggregateId >= 0, "Идентификатор сущности должен быть неотрицательным целым числом.");
			Argument.Require(expectedVersion >= -1, "Ожидаемая версия должна быть неотрицательным целым числом (или -1).");

			List<EventDescriptor> eventDescriptors;
			
			if (!_events.TryGetValue(aggregateId, out eventDescriptors))
			{
				eventDescriptors = new List<EventDescriptor>();
				_events.Add(aggregateId, eventDescriptors);
			}
			else if (eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion && expectedVersion != -1)
			{
				throw new Exception("Concurrency");
			}
			var i = expectedVersion;
			
			foreach (var @event in events)
			{
				i++;
				@event.Version = i;
				
				eventDescriptors.Add(new EventDescriptor(aggregateId, @event, i));
				
				_publisher.Publish(@event);
			}
		}

		/// <summary>
		/// Получить все события, связанные с сущностью.
		/// </summary>
		/// <param name="aggregateId">Идентификатор сущности.</param>
		/// <returns>Список событий.</returns>
		public List<IEvent> GetEventsForAggregate(int aggregateId)
		{
			Argument.Require(aggregateId >= 0, "Идентификатор сущности должен быть неотрицательным целым числом.");

			List<EventDescriptor> eventDescriptors;

			if (!_events.TryGetValue(aggregateId, out eventDescriptors))
			{
				throw new Exception($"Не найден объект с идентификатором {aggregateId}.");
			}

			return eventDescriptors.Select(desc => desc.EventData).ToList();
		}
	}
}
