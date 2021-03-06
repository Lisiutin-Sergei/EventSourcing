﻿using EventSourcing.Core.Domain;

namespace EventSourcing.Domain.User.Events
{
	/// <summary>
	/// Событие создания пользователя.
	/// </summary>
	public class UserCreatedEvent : IEvent
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; set; }

		public UserCreatedEvent(int userId, string name, string password)
		{
			UserId = userId;
			Name = name;
			Password = password;
		}
	}
}
