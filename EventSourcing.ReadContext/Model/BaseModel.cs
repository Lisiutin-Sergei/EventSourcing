namespace EventSourcing.ReadContext.Model
{
	/// <summary>
	/// Базовая доменная модель.
	/// </summary>
	public class BaseModel
	{
		/// <summary>
		/// Идентификатор сущности.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Версия.
		/// </summary>
		public int Version { get; set; }
	}
}
