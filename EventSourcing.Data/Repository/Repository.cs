using EventSourcing.Core.Domain;
using EventSourcing.Core.Repository;
using EventSourcing.Core.Utils;
using EventSourcing.Data.EventStore;

namespace EventSourcing.Data.Repository
{
	/// <summary>
	/// Репозиторий событий.
	/// </summary>
	/// <typeparam name="TAggregateRoot">Тип сущности, с которой работаем.</typeparam>
	public class Repository<TAggregateRoot> : IRepository<TAggregateRoot> 
		where TAggregateRoot : IAggregateRoot, new()
	{
		/// <summary>
		/// Хранилище событий.
		/// </summary>
		private readonly IEventStore _storage;

		public Repository(IEventStore storage)
		{
			Argument.NotNull(storage, "Не задано хранилище событий.");
			_storage = storage;
		}

		/// <summary>
		/// Сохранить изменения сущности.
		/// </summary>
		/// <param name="aggregate">Сущность.</param>
		/// <param name="expectedVersion">Ожидаемая версия.</param>
		public void Save(IAggregateRoot aggregate, int expectedVersion)
		{
			Argument.NotNull(aggregate, "Не задана сущность.");
			Argument.Require(expectedVersion >= -1, "Ожидаемая версия должна быть неотрицательным целым числом (или -1).");

			_storage.SaveEvents(aggregate.Id, aggregate.Changes, expectedVersion);
		}

		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		public TAggregateRoot GetById(int id)
		{
			Argument.Require(id >= 0, "Идентификатор сущности должен быть неотрицательным целым числом.");

			var obj = new TAggregateRoot();
			var e = _storage.GetEventsForAggregate(id);
			obj.LoadsFromHistory(e);
			return obj;
		}
	}
}
