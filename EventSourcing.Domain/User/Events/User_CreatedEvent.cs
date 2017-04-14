using EventSourcing.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventSourcing.Domain.User.Events
{
	public class User_CreatedEvent : IEvent
	{
		public int UserId { get; set; }

		public string Name { get; set; }

		public string Password { get; set; }

		public int Version { get; set; }

		public User_CreatedEvent(int userId, string name, string password)
		{
			UserId = userId;
			Name = name;
			Password = password;
		}
	}
}
