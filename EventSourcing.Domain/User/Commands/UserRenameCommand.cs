﻿using EventSourcing.Core.Domain;

namespace EventSourcing.Domain.User.Commands
{
	/// <summary>
	/// Команда смены имени пользователя.
	/// </summary>
	public class UserRenameCommand : ICommand
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
		/// Версия сущности.
		/// </summary>
		public int OriginalVersion { get; set; }

		public UserRenameCommand(int userId, string name, int originalVersion)
		{
			UserId = userId;
			Name = name;
			OriginalVersion = originalVersion;
		}

		/// <summary>
		/// Преобразование к строке.
		/// </summary>
		/// <returns>Строка с описанием команды.</returns>
		public override string ToString()
		{
			return 
				$"Команда смены имени пользователя на {Name ?? string.Empty} " +
				$"с идентификатором {UserId} успешно выполнена.";
		}
	}
}
