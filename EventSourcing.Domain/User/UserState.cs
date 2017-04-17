using EventSourcing.Core.Domain;
using EventSourcing.Core.MessageHandlers;
using EventSourcing.Domain.User.Events;

namespace EventSourcing.Domain.User
{
	/// <summary>
	/// Состояние сущности Пользователь.
	/// </summary>
	public class UserState : State,
		IEventHandler<User_CreatedEvent>,
		IEventHandler<User_PasswordChangedEvent>,
		IEventHandler<User_RenamedEvent>
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
		public void Handle(User_CreatedEvent @event)
		{
			Id = @event.UserId;
			Name = @event.Name;
			Password = @event.Password;
		}

		/// <summary>
		/// Обработать событие изменения паролья пользователя.
		/// </summary>
		/// <param name="event">Событие.</param>
		public void Handle(User_PasswordChangedEvent @event)
		{
			Password = @event.NewPassword;
		}

		/// <summary>
		/// Обработать событие изменения имени пользователя.
		/// </summary>
		/// <param name="event">Событие.</param>
		public void Handle(User_RenamedEvent @event)
		{
			Name = @event.NewName;
		}
	}
}
