using EventSourcing.Configurator;
using EventSourcing.Core.ServiceBus;
using EventSourcing.Domain;
using EventSourcing.Domain.User.Commands;
using EventSourcing.ReadContext;
using EventSourcing.ReadContext.ReadModel.Interface;
using Ninject;
using System;

namespace EventSourcing.Interface
{
	class Program
	{
		static void Main(string[] args)
		{
			var kernel = new StandardKernel();
			IoC.Instance.Initialize(kernel);
			IoC.Instance.RegisterServices();
			RegisterBusMessages();

			var serviceBus = IoC.Instance.Resolve<IServiceBus>();
			var userReadContext = IoC.Instance.Resolve<IUserReadContext>();
			var n = Environment.NewLine;

			var userId = 1;
			var cmd = new UserCreateCommand(userId, "Sergei", "Pass");
			serviceBus.Send(cmd);
			Console.WriteLine(cmd.ToString());
			userReadContext.GetAll().ForEach(u => Console.WriteLine($"{u.Id}\t{u.Name}\t{u.Password}{n}"));

			var userToRename = userReadContext.GetById(userId);
			var renameCmd = new UserRenameCommand(userToRename.Id, "Sergey", userToRename.Version);
			serviceBus.Send(renameCmd);
			Console.WriteLine(renameCmd.ToString());
			userReadContext.GetAll().ForEach(u => Console.WriteLine($"{u.Id}\t{u.Name}\t{u.Password}{n}"));

			var userToChangePassword = userReadContext.GetById(userId);
			var changePasswordCmd = new UserChangePasswordCommand(userToChangePassword.Id, "Pass", "Password", userToChangePassword.Version);
			serviceBus.Send(changePasswordCmd);
			Console.WriteLine(changePasswordCmd.ToString());
			userReadContext.GetAll().ForEach(u => Console.WriteLine($"{u.Id}\t{u.Name}\t{u.Password}{n}"));
		}

		/// <summary>
		/// Зарегистрировать обработчики команд/событий в сервисной шине.
		/// </summary>
		public static void RegisterBusMessages()
		{
			// Зарегать обработчики команд
			var domainRegistrator = IoC.Instance.Resolve<DomainRegistrator>();
			domainRegistrator.Register();

			// Зарегать обработчики событий read модели
			var readContextRegistrator = IoC.Instance.Resolve<ReadContextRegistrator>();
			readContextRegistrator.Register();
		}
	}
}
