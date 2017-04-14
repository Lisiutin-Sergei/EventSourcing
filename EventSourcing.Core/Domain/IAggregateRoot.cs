using System.Collections.Generic;

namespace EventSourcing.Core.Domain
{
	/// <summary>
	/// Интерфейс корня аггрегата сущностей.
	/// </summary>
	public interface IAggregateRoot
	{
		/// <summary>
		/// Идентификатор сущности.
		/// </summary>
		int Id { get; }

		/// <summary>
		/// Список изменений.
		/// </summary>
		List<IEvent> Changes { get; }

		/// <summary>
		/// Пометить изменения как выполненные - очистить список изменений.
		/// </summary>
		void MarkChangesAsCommitted();

		/// <summary>
		/// Применить список событий для сущности.
		/// </summary>
		/// <param name="history">Список событий.</param>
		void LoadsFromHistory(List<IEvent> history);
	}
}
