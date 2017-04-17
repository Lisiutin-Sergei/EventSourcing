using EventSourcing.Core.MessageHandlers;
using EventSourcing.Domain.User.Events;
using EventSourcing.ReadContext.Model;
using System;
using System.Linq;

namespace EventSourcing.ReadContext.EventHandlers
{
	/// <summary>
	/// Обработчики событий сущности Пользователь.
	/// </summary>
	public class UserEventHandler : 
		IEventHandler<User_CreatedEvent>,
		IEventHandler<User_PasswordChangedEvent>,
		IEventHandler<User_RenamedEvent>
	{
		/// <summary>
		/// Контекст данных для read модели.
		/// </summary>
		private readonly IStorageContext _storage;

		public UserEventHandler(IStorageContext storageContext)
		{
			_storage = storageContext;
		}

		/// <summary>
		/// Обработать событие создания пользователя.
		/// </summary>
		/// <param name="message">Событие создания пользователя.</param>
		public void Handle(User_CreatedEvent message)
		{
			_storage.Users.Add(new User
			{
				Id = message.UserId,
				Name = message.Name,
				Password = message.Password,
				Version = message.Version
			});
		}

		/// <summary>
		/// Обработать событие изменения пароля пользователя.
		/// </summary>
		/// <param name="message">Событие изменения пароля пользователя.</param>
		public void Handle(User_PasswordChangedEvent message)
		{
			var user = _storage.Users.FirstOrDefault(x => x.Id == message.UserId);
			if (user == null)
			{
				throw new Exception($"Не найден пользователь с идентификатором {message.UserId}.");
			}
			user.Password = message.NewPassword;
			user.Version = message.Version;
		}

		/// <summary>
		/// Обработать событие изменения имени пользователя.
		/// </summary>
		/// <param name="message">Событие изменения имени пользователя.</param>
		public void Handle(User_RenamedEvent message)
		{
			var user = _storage.Users.FirstOrDefault(x => x.Id == message.UserId);
			if (user == null)
			{
				throw new Exception($"Не найден пользователь с идентификатором {message.UserId}.");
			}
			user.Name = message.NewName;
			user.Version = message.Version;
		}
	}
}
