namespace EventSourcing.ReadContext.Model
{
	/// <summary>
	/// Модель пользователя.
	/// </summary>
	public class User : BaseModel
	{
		/// <summary>
		/// Имя пользователя.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Пароль пользователя.
		/// </summary>
		public string Password { get; set; }
	}
}
