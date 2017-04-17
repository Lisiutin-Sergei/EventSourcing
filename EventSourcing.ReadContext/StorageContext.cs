using EventSourcing.ReadContext.Model;
using System.Collections.Generic;

namespace EventSourcing.ReadContext
{
	/// <summary>
	/// Read хранилище данных.
	/// </summary>
	public class StorageContext : IStorageContext
	{
		/// <summary>
		/// Набор пользователей.
		/// </summary>
		public List<User> Users { get; private set; } = new List<User>();
	}
}
