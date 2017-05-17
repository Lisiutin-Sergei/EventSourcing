using EventSourcing.Core.Repository;
using EventSourcing.Core.ServiceBus;
using EventSourcing.Core.Utils;
using EventSourcing.Data.EventStore;
using EventSourcing.Data.Repository;
using EventSourcing.Data.ServiceBus;
using EventSourcing.ReadContext;
using EventSourcing.ReadContext.ReadModel.Implementation;
using EventSourcing.ReadContext.ReadModel.Interface;
using Ninject;
using Ninject.Parameters;

namespace EventSourcing.Configurator
{
	/// <summary>
	/// Конфигуратор контейнера зависимостей.
	/// </summary>
	public class IoC
	{
		protected IoC()
		{
		}
		private static IoC _instance;
		public static IoC Instance
		{
			get
			{
				return IoC._instance ?? (IoC._instance = new IoC());
			}
		}

		/// <summary>
		/// Кернел.
		/// </summary>
		protected IKernel _kernel;

		/// <summary>
		/// Инициализировать контейнер зависимостей.
		/// </summary>
		/// <param name="kernel">Ядро ninject.</param>
		public void Initialize(IKernel kernel)
		{
			Argument.NotNull(kernel, "Не задан объект для инициализации контейнера зависимостей.");
			_kernel = kernel;
		}

		/// <summary>
		/// Зарегистрировать зависимости.
		/// </summary>
		/// <param name="kernel">Ядро ninject.</param>
		public void RegisterServices()
		{
			_kernel.Bind<ServiceBus>().ToSelf().InSingletonScope();
			_kernel.Bind<IServiceBus>().ToMethod(c => c.Kernel.Get<ServiceBus>());
			_kernel.Bind<IEventPublisher>().ToMethod(c => c.Kernel.Get<ServiceBus>());
			_kernel.Bind<ICommandSender>().ToMethod(c => c.Kernel.Get<ServiceBus>());

			_kernel.Bind<IStorageContext>().To<StorageContext>().InSingletonScope();

			_kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));

			_kernel.Bind<IEventStore>().To<EventStore>().InSingletonScope();
			_kernel.Bind<IUserReadContext>().To<UserReadContext>();
		}

		/// <summary>
		/// Получить реализацию интерфейса.
		/// </summary>
		/// <typeparam name="S">Интрерфейс.</typeparam>
		/// <param name="arguments">Аргументы.</param>
		/// <returns>Реализация интерфейса.</returns>
		public S Resolve<S>(params NinjectArgument[] arguments)
		{
			return _kernel.Get<S>(arguments);
		}

		/// <summary>
		/// Создать аргумент ninject.
		/// </summary>
		/// <param name="name">Название аргумента.</param>
		/// <param name="value">Значение.</param>
		/// <returns>Аргумент.</returns>
		public static NinjectArgument GetArgument(string name, object value)
		{
			Argument.NotNullOrEmpty(name, "Не задано имя аргумента.");
			Argument.NotNull(value, "Не задано значение аргумента.");

			return new NinjectArgument(name, value);
		}

		/// <summary>
		/// Класс аргументов для ninject.
		/// </summary>
		public class NinjectArgument : ConstructorArgument
		{
			public NinjectArgument(string name, object value)
				: base(name, value)
			{
				Argument.NotNullOrEmpty(name, "Не задано имя аргумента.");
				Argument.NotNull(value, "Не задано значение аргумента.");
			}
		}
	}
}
