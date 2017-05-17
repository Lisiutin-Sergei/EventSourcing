using EventSourcing.Core.Utils;
using EventSourcing.ReadContext.Model;

namespace EventSourcing.ReadContext.ReadModel.Model
{
	/// <summary>
	/// Проекция пользователя.
	/// </summary>
	public class UserProjection
	{
		/// <summary>
		/// Идентификатор сущности.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Версия.
		/// </summary>
		public int Version { get; set; }

		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; set; }

		#region Ctor

		public UserProjection() { }

		/// <summary>
		/// Получить проекцию из базового объекта.
		/// </summary>
		/// <param name="user">Базовый объект типа Пользователь.</param>
		/// <remarks>Здесь мог бы быть адаптер =)</remarks>
		public UserProjection(User user)
		{
			Argument.NotNull(user, "Не указана базовая модель.");

			Id = user.Id;
			Version = user.Version;
			Name = user.Name;
			Password = user.Password;
		}

		#endregion
	}
}
