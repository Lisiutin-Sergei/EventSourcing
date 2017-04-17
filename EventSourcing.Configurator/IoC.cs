using EventSourcing.Core.Repository;
using EventSourcing.Core.ServiceBus;
using EventSourcing.Data.EventStore;
using EventSourcing.Data.Repository;
using EventSourcing.Data.ServiceBus;
using EventSourcing.ReadContext;
using Ninject;

namespace EventSourcing.Configurator
{
	/// <summary>
	/// Конфигуратор контейнера зависимостей.
	/// </summary>
	public class IoC
	{
		/// <summary>
		/// Кернел.
		/// </summary>
		public static IKernel Kernel { get; private set; }

		/// <summary>
		/// Зарегистрировать зависимости.
		/// </summary>
		/// <param name="kernel">Ядро ninject.</param>
		public static void RegisterServices(IKernel kernel)
		{
			Kernel = kernel;

			kernel.Bind<ServiceBus>().ToSelf().InSingletonScope();
			kernel.Bind<IServiceBus>().ToMethod(c => c.Kernel.Get<ServiceBus>());
			kernel.Bind<IEventPublisher>().ToMethod(c => c.Kernel.Get<ServiceBus>());
			kernel.Bind<ICommandSender>().ToMethod(c => c.Kernel.Get<ServiceBus>());

			kernel.Bind<IStorageContext>().To<StorageContext>().InSingletonScope();

			kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

			kernel.Bind<IEventStore>().To<EventStore>().InSingletonScope();
		}
	}
}
