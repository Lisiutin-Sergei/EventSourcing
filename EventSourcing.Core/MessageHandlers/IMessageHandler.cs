using EventSourcing.Core.Domain;

namespace EventSourcing.Core.MessageHandlers
{
	/// <summary>
	/// Базовый обработчик сообщений.
	/// </summary>
	/// <typeparam name="TMessageHandler">Тип сообщения.</typeparam>
	public interface IMessageHandler<TMessageHandler> 
		where TMessageHandler : IMessage
	{
		void Handle(TMessageHandler message);
	}
}
