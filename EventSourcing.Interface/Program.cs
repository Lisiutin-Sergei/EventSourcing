using EventSourcing.Configurator;
using EventSourcing.Core.ServiceBus;
using EventSourcing.Domain;
using EventSourcing.Domain.User.Commands;
using EventSourcing.ReadContext;
using Ninject;
using System;
using System.Linq;

namespace EventSourcing.Interface
{
	class Program
	{
		static void Main(string[] args)
		{
			var kernel = new StandardKernel();
			IoC.RegisterServices(kernel);
			RegisterBusMessages();

			var serviceBus = IoC.Kernel.Get<IServiceBus>();
			var readContext = IoC.Kernel.Get<IStorageContext>();

			var cmd = new User_CreateCommand(1, "Sergei", "Pass");
			serviceBus.Send(cmd);
			Console.WriteLine(cmd.ToString());
			readContext.Users.ForEach(u => Console.WriteLine($"{u.Id}\t{u.Name}\t{u.Password}"));

			var userToRename = readContext.Users.FirstOrDefault();
			var renameCmd = new User_RenameCommand(userToRename.Id, "Sergey", userToRename.Version);
			serviceBus.Send(renameCmd);
			Console.WriteLine(renameCmd.ToString());
			readContext.Users.ForEach(u => Console.WriteLine($"{u.Id}\t{u.Name}\t{u.Password}"));

			var changePasswordCmd = new User_ChangePasswordCommand(userToRename.Id, "Pass", "Password", userToRename.Version);
			serviceBus.Send(changePasswordCmd);
			Console.WriteLine(changePasswordCmd.ToString());
			readContext.Users.ForEach(u => Console.WriteLine($"{u.Id}\t{u.Name}\t{u.Password}"));
		}

		/// <summary>
		/// Зарегистрировать обработчики команд/событий в сервисной шине. Здесь должно быть дофига DI.
		/// </summary>
		/// <returns>Шина + read хранилище.</returns>
		public static void RegisterBusMessages()
		{
			// Зарегать обработчики команд
			var domainRegistrator = IoC.Kernel.Get<DomainRegistrator>();
			domainRegistrator.Register();

			// Зарегать обработчики событий read модели
			var readContextRegistrator = IoC.Kernel.Get<ReadContextRegistrator>();
			readContextRegistrator.Register();
		}
	}
}
