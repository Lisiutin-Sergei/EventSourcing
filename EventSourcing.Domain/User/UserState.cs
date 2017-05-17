using EventSourcing.Core.Domain;
using EventSourcing.Core.MessageHandlers;
using EventSourcing.Core.Utils;
using EventSourcing.Domain.User.Events;

namespace EventSourcing.Domain.User
{
	/// <summary>
	/// Состояние сущности Пользователь.
	/// </summary>
	public class UserState : State,
		IEventHandler<UserCreatedEvent>,
		IEventHandler<UserPasswordChangedEvent>,
		IEventHandler<UserRenamedEvent>
	{
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; private set; }

		/// <summary>
		/// Обработать событие создания пользователя.
		/// </summary>
		/// <param name="event">Событие.</param>
		public void Handle(UserCreatedEvent @event)
		{
			Argument.NotNull(@event, "Не задано событие.");

			Id = @event.UserId;
			Name = @event.Name;
			Password = @event.Password;
		}

		/// <summary>
		/// Обработать событие изменения паролья пользователя.
		/// </summary>
		/// <param name="event">Событие.</param>
		public void Handle(UserPasswordChangedEvent @event)
		{
			Argument.NotNull(@event, "Не задано событие.");

			Password = @event.NewPassword;
		}

		/// <summary>
		/// Обработать событие изменения имени пользователя.
		/// </summary>
		/// <param name="event">Событие.</param>
		public void Handle(UserRenamedEvent @event)
		{
			Argument.NotNull(@event, "Не задано событие.");

			Name = @event.NewName;
		}
	}
}
