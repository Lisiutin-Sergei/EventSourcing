using EventSourcing.Core.Utils;
using EventSourcing.ReadContext.ReadModel.Interface;
using EventSourcing.ReadContext.ReadModel.Model;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcing.ReadContext.ReadModel.Implementation
{
	/// <summary>
	/// Read контекст для работы с пользователями.
	/// </summary>
	public class UserReadContext : IUserReadContext
	{
		private readonly IStorageContext _context;

		public UserReadContext(IStorageContext context)
		{
			Argument.NotNull(context, "Не задано хранилище данных.");

			_context = context;
		}

		/// <summary>
		/// Получить пользователя по Id.
		/// </summary>
		/// <param name="userId">Идентиификатор пользователя.</param>
		/// <returns></returns>
		public UserProjection GetById(int userId)
		{
			var user = _context.Users
				.FirstOrDefault(u => u.Id == userId);
			if (user == null)
			{
				return null;
			}
			return new UserProjection(user);
		}

		/// <summary>
		/// Получить всех пользователей.
		/// </summary>
		/// <returns>Список пользователей.</returns>
		public List<UserProjection> GetAll()
		{
			return _context.Users
				.Select(u => new UserProjection(u))
				.ToList();
		}
	}
}
