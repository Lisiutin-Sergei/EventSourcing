using EventSourcing.ReadContext.ReadModel.Model;
using System.Collections.Generic;

namespace EventSourcing.ReadContext.ReadModel.Interface
{
	/// <summary>
	/// Интерфейс Read контекста для работы с пользователями.
	/// </summary>
	public interface IUserReadContext
	{
		/// <summary>
		/// Получить пользователя по Id.
		/// </summary>
		/// <param name="userId">Идентиификатор пользователя.</param>
		/// <returns></returns>
		UserProjection GetById(int userId);

		/// <summary>
		/// Получить всех пользователей.
		/// </summary>
		/// <returns>Список пользователей.</returns>
		List<UserProjection> GetAll();
	}
}
