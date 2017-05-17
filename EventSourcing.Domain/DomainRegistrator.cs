using EventSourcing.Core.Repository;
using EventSourcing.Core.ServiceBus;
using EventSourcing.Core.Utils;
using EventSourcing.Domain.User;
using EventSourcing.Domain.User.Commands;

namespace EventSourcing.Domain
{
	/// <summary>
	/// Регистратор обработчиков команд.
	/// </summary>
	public class DomainRegistrator
	{
		/// <summary>
		/// Сервисная шина.
		/// </summary>
		private readonly IServiceBus _serviceBus;

		/// <summary>
		/// Репозиторий событий для сущности Пользователь.
		/// </summary>
		private IRepository<UserAggregateRoot> _userAggregateRepository { get; set; }

		public DomainRegistrator(IServiceBus serviceBus, IRepository<UserAggregateRoot> userAggregateRepository)
		{
			Argument.NotNull(serviceBus, "Не задана сервисная шина.");
			Argument.NotNull(userAggregateRepository, "Не задан репозиторий событий для сущности Пользователь.");

			_serviceBus = serviceBus;
			_userAggregateRepository = userAggregateRepository;
		}

		/// <summary>
		/// Зарегать обработчики команд.
		/// </summary>
		public void Register()
		{
			// Зарегать обработчики команд для сущности Пользователь
			var userCommands = new UserCommandHandler(_userAggregateRepository);
			_serviceBus.RegisterHandler<UserCreateCommand>(userCommands.Handle);
			_serviceBus.RegisterHandler<UserRenameCommand>(userCommands.Handle);
			_serviceBus.RegisterHandler<UserChangePasswordCommand>(userCommands.Handle);
		}
	}
}
