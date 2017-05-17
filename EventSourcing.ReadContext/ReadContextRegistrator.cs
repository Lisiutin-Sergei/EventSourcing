using EventSourcing.Core.ServiceBus;
using EventSourcing.Core.Utils;
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
			Argument.NotNull(serviceBus, "Не задана сервисная шина.");
			Argument.NotNull(storageContext, "Не задано хранилище данных.");

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
			_serviceBus.RegisterHandler<UserCreatedEvent>(readUserEventHandler.Handle);
			_serviceBus.RegisterHandler<UserRenamedEvent>(readUserEventHandler.Handle);
			_serviceBus.RegisterHandler<UserPasswordChangedEvent>(readUserEventHandler.Handle);
		}
	}
}
