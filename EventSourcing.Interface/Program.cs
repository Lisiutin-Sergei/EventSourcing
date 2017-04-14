using EventSourcing.Core.ServiceBus;
using EventSourcing.Data.EventStore;
using EventSourcing.Data.Repository;
using EventSourcing.Data.ServiceBus;
using EventSourcing.Domain.User;
using EventSourcing.Domain.User.Commands;

namespace EventSourcing.Interface
{
	class Program
	{
		static void Main(string[] args)
		{
			var cmd = new User_CreateCommand(1, "Sergei", "Pass");
			var serviceBus = RegisterBus();
			serviceBus.Send(cmd);
		}

		public static IServiceBus RegisterBus()
		{
			var bus = new ServiceBus();
			var storage = new EventStore(bus);
			var rep = new Repository<UserAggregateRoot>(storage);
			var commands = new UserCommandHandler(rep);
			bus.RegisterHandler<User_CreateCommand>(commands.Handle);
			return bus;
		}
	}
}
