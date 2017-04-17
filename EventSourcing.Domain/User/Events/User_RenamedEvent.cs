﻿using EventSourcing.Core.Domain;

namespace EventSourcing.Domain.User.Events
{
	/// <summary>
	/// События изменения имени пользователя.
	/// </summary>
	public class User_RenamedEvent : IEvent
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Новое имя пользователя.
		/// </summary>
		public string NewName { get; set; }

		/// <summary>
		/// Старое имя пользователя.
		/// </summary>
		public string OldName { get; set; }

		public User_RenamedEvent(int userId, string newName, string oldName)
		{
			UserId = userId;
			NewName = newName;
			OldName = oldName;
		}
	}
}
