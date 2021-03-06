﻿using EventSourcing.Core.Domain;

namespace EventSourcing.Core.Repository
{
	/// <summary>
	/// Интерфейс репозитория событий.
	/// </summary>
	/// <typeparam name="TAggregateRoot">Тип сущности, с которой работаем.</typeparam>
	public interface IRepository<TAggregateRoot> 
		where TAggregateRoot : IAggregateRoot, new()
	{
		/// <summary>
		/// Сохранить изменения сущности.
		/// </summary>
		/// <param name="aggregate">Сущность.</param>
		/// <param name="expectedVersion">Ожидаемая версия.</param>
		void Save(IAggregateRoot aggregate, int expectedVersion);

		/// <summary>
		/// Получить сущность по идентификатору.
		/// </summary>
		/// <param name="id">Идентификатор сущности.</param>
		/// <returns>Сущность.</returns>
		TAggregateRoot GetById(int id);
	}
}
