using EventSourcing.Core.MessageHandlers;
using EventSourcing.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Domain.User.Commands
{
	public class UserCommandHandler : ICommandHandler<User_CreateCommand>
	{
		private readonly IRepository<UserAggregateRoot> _repository;

		public UserCommandHandler(IRepository<UserAggregateRoot> repository)
		{
			_repository = repository;
		}

		public void Handle(User_CreateCommand message)
		{
			var ar = new UserAggregateRoot(message.UserId, message.Name, message.Password);
			_repository.Save(ar, -1);
		}
	}
}
