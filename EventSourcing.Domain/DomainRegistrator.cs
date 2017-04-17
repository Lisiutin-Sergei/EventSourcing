using EventSourcing.Core.Repository;
using EventSourcing.Core.ServiceBus;
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
			_serviceBus = serviceBus;
			_userAggregateRepository = userAggregateRepository;
		}

		/// <summary>
		/// Зарегать обработчики команд.
		/// </summary>
		/// <param name="serviceBus">Сервисная шина.</param>
		public void Register()
		{
			// Зарегать обработчики команд для сущности Пользователь
			var userCommands = new UserCommandHandler(_userAggregateRepository);
			_serviceBus.RegisterHandler<User_CreateCommand>(userCommands.Handle);
			_serviceBus.RegisterHandler<User_RenameCommand>(userCommands.Handle);
			_serviceBus.RegisterHandler<User_ChangePasswordCommand>(userCommands.Handle);
		}
	}
}
