using EventSourcing.Core.ServiceBus;
using EventSourcing.Domain.User.Events;
using EventSourcing.ReadContext.EventHandlers;

namespace EventSourcing.ReadContext
{
	/// <summary>
	/// Регистратор обработчиков событий read модели.
	/// </summary>
	public class ReadContextRegistrator
	{
		/// <summary>
		/// Сервисная шина.
		/// </summary>
		private readonly IServiceBus _serviceBus;

		/// <summary>
		/// Контекст данных read модели.
		/// </summary>
		private readonly IStorageContext _storageContext;

		public ReadContextRegistrator(IServiceBus serviceBus, IStorageContext storageContext)
		{
			_serviceBus = serviceBus;
			_storageContext = storageContext;
		}

		/// <summary>
		/// Зарегистрировать обработчики событий read модели.
		/// </summary>
		public void Register()
		{
			// Зарегать обработчики событий read модели для сущности Пользователь
			var readUserEventHandler = new UserEventHandler(_storageContext);
			_serviceBus.RegisterHandler<User_CreatedEvent>(readUserEventHandler.Handle);
			_serviceBus.RegisterHandler<User_RenamedEvent>(readUserEventHandler.Handle);
			_serviceBus.RegisterHandler<User_PasswordChangedEvent>(readUserEventHandler.Handle);
		}
	}
}
