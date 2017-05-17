using EventSourcing.Core.Domain;
using System;

namespace EventSourcing.Core.ServiceBus
{
	/// <summary>
	/// Интерфейс сервисной шины.
	/// </summary>
	public interface IServiceBus : ICommandSender, IEventPublisher
	{
		/// <summary>
		/// Зарегистрировать обработчики сообщений.
		/// </summary>
		/// <typeparam name="TMessage">Тип сообщения.</typeparam>
		/// <param name="handler">Сообщение.</param>
		void RegisterHandler<TMessage>(Action<TMessage> handler)
			where TMessage : IMessage;
	}
}
