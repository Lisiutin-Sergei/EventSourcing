using EventSourcing.Core.Domain;
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
			var @event = new User_CreatedEvent(userId, name, password);
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
			if (string.IsNullOrEmpty(newPassword))
			{
				throw new ArgumentException("newPassword");
			}
			ApplyChange(new User_PasswordChangedEvent(Id, oldPassword, newPassword));
		}

		/// <summary>
		/// Изменить имя пользователя.
		/// </summary>
		/// <param name="newPassword">Новый пароль.</param>
		/// <param name="oldPassword">Старый пароль.</param>
		public void Rename(string newName)
		{
			if (string.IsNullOrEmpty(newName))
			{
				throw new ArgumentException("newName");
			}
			ApplyChange(new User_RenamedEvent(Id, newName, State.Name));
		}
	}
}
