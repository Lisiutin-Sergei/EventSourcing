using EventSourcing.Core.Domain;
using EventSourcing.Core.Utils;
using EventSourcing.Domain.User.Events;
using System;

namespace EventSourcing.Domain.User
{
	/// <summary>
	/// Корень аггрегата сущности Пользователь.
	/// </summary>
	public class UserAggregateRoot : AggregateRoot<UserState>
	{
		public UserAggregateRoot(int userId, string name, string password)
		{
			var @event = new UserCreatedEvent(userId, name, password);
			ApplyChange(@event);
		}

		public UserAggregateRoot() { }

		/// <summary>
		/// Изменить пароль пользователя.
		/// </summary>
		/// <param name="newPassword">Новый пароль.</param>
		/// <param name="oldPassword">Старый пароль.</param>
		public void ChangePassword(string newPassword, string oldPassword)
		{
			Argument.NotNull(newPassword, "Не задан новый пароль.");

			ApplyChange(new UserPasswordChangedEvent(Id, oldPassword, newPassword));
		}

		/// <summary>
		/// Изменить имя пользователя.
		/// </summary>
		/// <param name="newPassword">Новое имя пользователя.</param>
		/// <param name="oldPassword">Старое имя пользователя.</param>
		public void Rename(string newName)
		{
			Argument.NotNull(newName, "Не задано новое имя пользователя.");

			ApplyChange(new UserRenamedEvent(Id, newName, State.Name));
		}
	}
}
