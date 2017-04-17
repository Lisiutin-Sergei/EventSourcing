using System.Collections.Generic;

namespace EventSourcing.Core.Domain
{
	/// <summary>
	/// Корень аггрегата сущности.
	/// </summary>
	public abstract class AggregateRoot<T> : IAggregateRoot where T : State, new()
	{
		/// <summary>
		/// Идентификатор сущности.
		/// </summary>
		public int Id => State?.Id ?? -1;

		/// <summary>
		/// Список изменений.
		/// </summary>
		public List<IEvent> Changes { get; private set; } = new List<IEvent>();

		/// <summary>
		/// Состояние сущности.
		/// </summary>
		public T State { get; private set; } = new T();

		/// <summary>
		/// Пометить изменения как выполненные - очистить список изменений.
		/// </summary>
		public void MarkChangesAsCommitted()
		{
			Changes.Clear();
		}

		/// <summary>
		/// Применить список событий для сущности.
		/// </summary>
		/// <param name="history">Список событий.</param>
		public void LoadsFromHistory(List<IEvent> history)
		{
			foreach (var e in history)
			{
				ApplyChange(e, false);
			}
		}

		/// <summary>
		/// Прмиенить изменения события к состоянию сущности.
		/// </summary>
		/// <param name="event">Событие.</param>
		/// <param name="needSetChanges">Нужно ли отображать изменения в списке изменений.</param>
		public void ApplyChange(IEvent @event, bool needSetChanges = true)
		{
			State.Mutate(@event);
			if (needSetChanges)
			{
				Changes.Add(@event);
			}
		}
	}
}
