using EventSourcing.Core.Domain;
using EventSourcing.Domain.User.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Domain.User
{
	public class UserAggregateRoot : AggregateRoot<UserState>
	{
		public UserAggregateRoot(int userId, string name, string password)
		{
			var @event = new User_CreatedEvent(userId, name, password);
			ApplyChange(@event);
		}

		public UserAggregateRoot() { }
	}
}
