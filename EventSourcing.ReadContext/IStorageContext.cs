using EventSourcing.ReadContext.Model;
using System.Collections.Generic;

namespace EventSourcing.ReadContext
{
	/// <summary>
	/// Интерфейс хранилища данных.
	/// </summary>
	public interface IStorageContext
	{
		/// <summary>
		/// Набор пользователей.
		/// </summary>
		List<User> Users { get; }
	}
}
