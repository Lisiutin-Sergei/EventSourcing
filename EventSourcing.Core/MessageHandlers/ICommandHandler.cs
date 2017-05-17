using EventSourcing.Core.Domain;

namespace EventSourcing.Core.MessageHandlers
{
	/// <summary>
	/// Интерфейс обработчика команд.
	/// </summary>
	/// <typeparam name="TCommandHandler">Тип команды.</typeparam>
	public interface ICommandHandler<TCommandHandler> : IMessageHandler<TCommandHandler> 
		where TCommandHandler : ICommand
	{
	}
}
