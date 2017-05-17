using EventSourcing.Core.MessageHandlers;
using EventSourcing.Core.Utils;
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
		IEventHandler<UserCreatedEvent>,
		IEventHandler<UserPasswordChangedEvent>,
		IEventHandler<UserRenamedEvent>
	{
		/// <summary>
		/// Контекст данных для read модели.
		/// </summary>
		private readonly IStorageContext _storage;

		public UserEventHandler(IStorageContext storageContext)
		{
			Argument.NotNull(storageContext, "Не задан контекст данных для read модели.");

			_storage = storageContext;
		}

		/// <summary>
		/// Обработать событие создания пользователя.
		/// </summary>
		/// <param name="message">Событие создания пользователя.</param>
		public void Handle(UserCreatedEvent message)
		{
			Argument.NotNull(message, "Не задано событие создания пользователя.");

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
		public void Handle(UserPasswordChangedEvent message)
		{
			Argument.NotNull(message, "Не задано событие изменения пароля пользователя.");

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
		public void Handle(UserRenamedEvent message)
		{
			Argument.NotNull(message, "Не задано событие изменения имени пользователя.");

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
