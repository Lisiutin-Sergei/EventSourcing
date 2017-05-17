using EventSourcing.Core.Domain;

namespace EventSourcing.Domain.User.Commands
{
	/// <summary>
	/// Команда смены пароля пользователя.
	/// </summary>
	public class UserChangePasswordCommand : ICommand
	{
		/// <summary>
		/// Идентификатор пользователя.
		/// </summary>
		public int UserId { get; set; }

		/// <summary>
		/// Старый пароль.
		/// </summary>
		public string OldPassword { get; set; }

		/// <summary>
		/// Новый пароль.
		/// </summary>
		public string NewPassword { get; set; }

		/// <summary>
		/// Версия сущности.
		/// </summary>
		public int OriginalVersion { get; set; }

		public UserChangePasswordCommand(int userId, string oldPassword, string newPassword, int originalVersion)
		{
			UserId = userId;
			OldPassword = oldPassword;
			NewPassword = newPassword;
			OriginalVersion = originalVersion;
		}

		/// <summary>
		/// Преобразование к строке.
		/// </summary>
		/// <returns>Строка с описанием команды.</returns>
		public override string ToString()
		{
			return 
				$"Команда смены пароля с {OldPassword ?? string.Empty} на {NewPassword ?? string.Empty}" +
				$" у пользователя с идентификатором {UserId} успешно выполнена.";
		}
	}
}
