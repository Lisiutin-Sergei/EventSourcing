using EventSourcing.Core.Domain;
using EventSourcing.Core.MessageHandlers;
using EventSourcing.Domain.User.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Domain.User
{
	public class UserState : State,
			IEventHandler<User_CreatedEvent>
	{
		public string Name { get; private set; }
		public string Password { get; private set; }

		public void Handle(User_CreatedEvent userCreated)
		{
			Id = userCreated.UserId;
			Name = userCreated.Name;
			Password = userCreated.Password;
		}
	}
}
