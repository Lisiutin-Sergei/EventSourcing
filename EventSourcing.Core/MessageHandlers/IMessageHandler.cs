using EventSourcing.Core.Domain;

namespace EventSourcing.Core.MessageHandlers
{
	/// <summary>
	/// Базовый обработчик сообщений.
	/// </summary>
	/// <typeparam name="T">Тип сообщения.</typeparam>
	public interface IMessageHandler<T> where T : IMessage
	{
		void Handle(T message);
	}
}
