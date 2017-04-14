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
		/// <typeparam name="T">Тип сообщения.</typeparam>
		/// <param name="handler">Сообщение.</param>
		void RegisterHandler<T>(Action<T> handler) where T : IMessage;
	}
}
