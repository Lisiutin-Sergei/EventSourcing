using EventSourcing.Core.MessageHandlers;
using EventSourcing.Core.Repository;
using EventSourcing.Core.Utils;

namespace EventSourcing.Domain.User.Commands
{
	/// <summary>
	/// Обработчик команд пользователя.
	/// </summary>
	public class UserCommandHandler : 
		ICommandHandler<UserCreateCommand>,
		ICommandHandler<UserRenameCommand>,
		ICommandHandler<UserChangePasswordCommand>
	{
		/// <summary>
		/// Репозиторий событий.
		/// </summary>
		private readonly IRepository<UserAggregateRoot> _repository;
		
		public UserCommandHandler(IRepository<UserAggregateRoot> repository)
		{
			Argument.NotNull(repository, "Не задан репозиторий.");
			_repository = repository;
		}

		/// <summary>
		/// Обработать команду создания пользователя.
		/// </summary>
		/// <param name="message">Команда.</param>
		public void Handle(UserCreateCommand message)
		{
			Argument.NotNull(message, "Не задана команда.");

			var user = new UserAggregateRoot(message.UserId, message.Name, message.Password);
			_repository.Save(user, -1);
		}

		/// <summary>
		/// Обработать команду изменения имени пользователя.
		/// </summary>
		/// <param name="message">Команда.</param>
		public void Handle(UserRenameCommand message)
		{
			Argument.NotNull(message, "Не задана команда.");

			var user = _repository.GetById(message.UserId);
			user.Rename(message.Name);
			_repository.Save(user, message.OriginalVersion);
		}

		/// <summary>
		/// Обработать команду изменения пароля пользователя.
		/// </summary>
		/// <param name="message">Команда.</param>
		public void Handle(UserChangePasswordCommand message)
		{
			Argument.NotNull(message, "Не задана команда.");

			var user = _repository.GetById(message.UserId);
			user.ChangePassword(message.NewPassword, message.OldPassword);
			_repository.Save(user, message.OriginalVersion);
		}
	}
}
