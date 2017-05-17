using EventSourcing.Core.Domain;

namespace EventSourcing.Domain.User.Events
{
	/// <summary>
	/// Событие изменения пароля.
	/// </summary>
	public class UserPasswordChangedEvent : IEvent
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

		public UserPasswordChangedEvent(int userId, string oldPassword, string newPassword)
		{
			UserId = userId;
			OldPassword = oldPassword;
			NewPassword = newPassword;
		}
	}
}
