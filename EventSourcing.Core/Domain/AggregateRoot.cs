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
		public int Id => _state?.Id ?? -1;

		/// <summary>
		/// Список изменений.
		/// </summary>
		private readonly List<IEvent> _changes = new List<IEvent>();
		public List<IEvent> Changes => _changes;

		/// <summary>
		/// Состояние сущности.
		/// </summary>
		private readonly T _state = new T();
		public T State => _state;

		/// <summary>
		/// Пометить изменения как выполненные - очистить список изменений.
		/// </summary>
		public void MarkChangesAsCommitted()
		{
			_changes.Clear();
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
				_changes.Add(@event);
			}
		}
	}
}
