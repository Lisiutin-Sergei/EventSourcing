using EventSourcing.Core.Domain;

namespace EventSourcing.Domain.User.Commands
{
	/// <summary>
	/// Команда создания пользователя.
	/// </summary>
	public class UserCreateCommand : ICommand
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

		public UserCreateCommand(int userId, string name, string password)
		{
			UserId = userId;
			Name = name;
			Password = password;
		}

		/// <summary>
		/// Преобразование к строке.
		/// </summary>
		/// <returns>Строка с описанием команды.</returns>
		public override string ToString()
		{
			return 
				$"Команда создания пользователя {Name ?? string.Empty} с идентификатором {UserId}" +
				$" и паролем {Password ?? string.Empty} успешно выполнена.";
		}
	}
}
