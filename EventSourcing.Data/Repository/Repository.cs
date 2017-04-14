using EventSourcing.Core.Domain;
using EventSourcing.Core.Repository;
using EventSourcing.Data.EventStore;

namespace EventSourcing.Data.Repository
{
	/// <summary>
	/// Репозиторий событий.
	/// </summary>
	/// <typeparam name="T">Тип сущности, с которой работаем.</typeparam>
	public class Repository<T> : IRepository<T> where T : IAggregateRoot, new()
	{
		/// <summary>
		/// Хранилище событий.
		/// </summary>
		private readonly IEventStore _storage;

		public Repository(IEventStore storage)
		{
			_storage = storage;
		}

		/// <summary>
		/// Сохранить изменения сущности.
		/// </summary>
		/// <param name="aggregate">Сущность.</param>
		/// <param name="expectedVersion">Ожидаемая версия.</param>
		public void Save(IAggregateRoot aggregate, int expectedVersion)
		{
			_storage.SaveEvents(aggregate.Id, aggregate.Changes, expectedVersion);
		}

		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		public T GetById(int id)
		{
			var obj = new T();
			var e = _storage.GetEventsForAggregate(id);
			obj.LoadsFromHistory(e);
			return obj;
		}
	}
}
