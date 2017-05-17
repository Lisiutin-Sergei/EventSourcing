using EventSourcing.Core.Utils;

namespace EventSourcing.Core.Domain
{
	/// <summary>
	/// Базовый класс состояния сущности.
	/// </summary>
	public abstract class State
	{
		/// <summary>
		/// Идентификатор сущности.
		/// </summary>
		public int Id { get; protected set; }

		/// <summary>
		/// Применить изменения, спровоцированные событием.
		/// </summary>
		/// <param name="event">Событие.</param>
		public void Mutate(IEvent @event)
		{
			Argument.NotNull(@event, "Не задано событие.");

			((dynamic)this).Handle((dynamic)@event);
		}
	}
}
