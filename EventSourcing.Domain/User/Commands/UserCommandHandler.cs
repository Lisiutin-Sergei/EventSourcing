using EventSourcing.Core.MessageHandlers;
using EventSourcing.Core.Repository;

namespace EventSourcing.Domain.User.Commands
{
	/// <summary>
	/// Обработчик команд пользователя.
	/// </summary>
	public class UserCommandHandler : 
		ICommandHandler<User_CreateCommand>,
		ICommandHandler<User_RenameCommand>,
		ICommandHandler<User_ChangePasswordCommand>
	{
		/// <summary>
		/// Репозиторий событий.
		/// </summary>
		private readonly IRepository<UserAggregateRoot> _repository;
		
		public UserCommandHandler(IRepository<UserAggregateRoot> repository)
		{
			_repository = repository;
		}

		/// <summary>
		/// Обработать команду создания пользователя.
		/// </summary>
		/// <param name="message">Команда.</param>
		public void Handle(User_CreateCommand message)
		{
			var user = new UserAggregateRoot(message.UserId, message.Name, message.Password);
			_repository.Save(user, -1);
		}

		/// <summary>
		/// Обработать команду изменения имени пользователя.
		/// </summary>
		/// <param name="message">Команда.</param>
		public void Handle(User_RenameCommand message)
		{
			var user = _repository.GetById(message.UserId);
			user.Rename(message.Name);
			_repository.Save(user, message.OriginalVersion);
		}

		/// <summary>
		/// Обработать команду изменения пароля пользователя.
		/// </summary>
		/// <param name="message">Команда.</param>
		public void Handle(User_ChangePasswordCommand message)
		{
			var user = _repository.GetById(message.UserId);
			user.ChangePassword(message.NewPassword, message.OldPassword);
			_repository.Save(user, message.OriginalVersion);
		}
	}
}
